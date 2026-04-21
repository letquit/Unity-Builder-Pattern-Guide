using UnityEngine;

/// <summary>
/// 近战武器策略。
/// 具体的武器实现，定义了近战攻击的行为。
/// 可以在编辑器中创建为资产（例如：长剑、斧头）。
/// </summary>
[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Enemy/WeaponStrategy/Melee")]
public class MeleeWeaponStrategy : WeaponStrategy
{
    /// <summary>
    /// 执行近战攻击。
    /// </summary>
    /// <param name="attacker">发起攻击的敌人</param>
    /// <param name="target">攻击目标</param>
    public override void Attack(Transform attacker, Transform target)
    {
        // 1. 检查目标是否在攻击范围内
        // 如果不在范围内，直接返回，不执行后续逻辑
        if (!IsInRange(attacker, target)) return;
        
        // 2. 尝试获取目标的生命值组件
        if (target.TryGetComponent<Health>(out var health))
        {
            // 3. 如果目标有血条，造成伤害
            health.TakeDamage(Damage);
            
            // 4. 记录攻击日志
            Debug.Log($"{attacker.name} 近战攻击 {target.name} 造成 {Damage} 伤害");
        }
        // 如果目标没有 Health 组件，则什么也不做（无法造成伤害）
    }
}