using UnityEngine;

/// <summary>
/// 敌人武器控制器。
/// 使用策略模式管理不同的攻击方式，处理攻击冷却和逻辑委托。
/// </summary>
public class EnemyWeapon : MonoBehaviour
{
    private WeaponStrategy _currentStrategy; // 当前的武器策略（如近战、远程）
    private float _nextAttackTime; // 下一次允许攻击的时间点
    
    // 只读属性：获取当前策略
    public WeaponStrategy CurrentStrategy => _currentStrategy;
    
    /// <summary>
    /// 设置武器策略。
    /// 允许在运行时动态切换攻击方式。
    /// </summary>
    /// <param name="strategy">新的武器策略</param>
    public void SetWeaponStrategy(WeaponStrategy strategy)
    {
        _currentStrategy = strategy;
    }
    
    /// <summary>
    /// 尝试攻击目标。
    /// 检查冷却时间和策略有效性，然后执行攻击。
    /// </summary>
    /// <param name="target">攻击目标</param>
    /// <returns>如果攻击成功返回 true，否则返回 false</returns>
    public bool TryAttack(Transform target)
    {
        // 检查策略是否已设置
        if (_currentStrategy == null)
        {
            Debug.LogWarning($"{name} 未设置 WeaponStrategy!");
            return false;
        }
        
        // 检查攻击冷却
        if (Time.time < _nextAttackTime)
            return false;
        
        // 委托给策略类执行具体的攻击逻辑
        _currentStrategy.Attack(transform, target);
        
        // 更新下一次攻击时间
        _nextAttackTime = Time.time + _currentStrategy.AttackRate;
        return true;
    }
    
    // 属性透传：如果策略为空，返回默认安全值
    public int Damage => _currentStrategy?.Damage ?? 0;
    public float AttackRate => _currentStrategy?.AttackRate ?? 1f;
    public float AttackRange => _currentStrategy?.AttackRange ?? 2f;
    
#if UNITY_EDITOR
    /// <summary>
    /// 编辑器专用方法。
    /// 当脚本或组件在编辑器中被修改时调用，用于调试。
    /// </summary>
    private void OnValidate()
    {
        if (_currentStrategy != null)
        {
            Debug.Log($"[{name}] 当前武器策略: {_currentStrategy.name}");
        }
    }
#endif
}