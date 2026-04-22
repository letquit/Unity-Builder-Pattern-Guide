using UnityEngine;

/// <summary>
/// 武器策略基类。
/// 定义所有武器攻击行为的通用接口和数据。
/// 继承自 ScriptableObject，允许作为资产在编辑器中配置。
/// </summary>
public abstract class WeaponStrategy : ScriptableObject
{
    [Header("Weapon Stats")] // 在 Inspector 中显示为分组标题
    [SerializeField] protected int damage = 10; // 伤害数值
    [SerializeField] protected float attackRate = 1f; // 攻击间隔（秒）
    [SerializeField] protected float attackRange = 2f; // 攻击范围
    
    // 虚属性，允许子类重写
    public virtual int Damage => damage;
    public virtual float AttackRate => attackRate;
    public virtual float AttackRange => attackRange;
    
    /// <summary>
    /// 执行攻击逻辑。
    /// 这是一个抽象方法，具体的攻击行为（如造成伤害、播放动画）由子类实现。
    /// </summary>
    /// <param name="attacker">发起攻击者的 Transform</param>
    /// <param name="target">攻击目标的 Transform</param>
    public abstract void Attack(Transform attacker, Transform target);
    
    /// <summary>
    /// 检查目标是否在攻击范围内。
    /// </summary>
    /// <param name="attacker">发起攻击者</param>
    /// <param name="target">目标</param>
    /// <returns>如果目标在范围内返回 true，否则返回 false</returns>
    protected bool IsInRange(Transform attacker, Transform target)
    {
        if (target == null) return false; // 空目标视为不在范围内
        // 计算两点间距离并比较
        return Vector3.Distance(attacker.position, target.position) <= attackRange;
    }
}