using UnityEngine;

/// <summary>
/// 装备工厂。
/// 一个组合式工厂，用于统一管理和生成角色的全套装备（武器、盾牌等）。
/// 继承自 ScriptableObject，支持在编辑器中可视化配置。
/// </summary>
[CreateAssetMenu(fileName = "EquipmentFactory", menuName = "EquipmentFactory")]
public class EquipmentFactory : ScriptableObject
{
    // 武器子工厂：负责具体生成哪种武器
    public WeaponFactory weaponFactory;
    
    // 盾牌子工厂：负责具体生成哪种盾牌
    public ShieldFactory shieldFactory;
    
    /// <summary>
    /// 创建武器。
    /// 如果配置了子工厂，则使用子工厂创建；否则使用默认武器（兜底策略）。
    /// </summary>
    public IWeapon CreateWeapon()
    {
        return weaponFactory != null ? weaponFactory.CreateWeapon() : IWeapon.CreateDefault();
    }
    
    /// <summary>
    /// 创建盾牌。
    /// 如果配置了子工厂，则使用子工厂创建；否则使用默认盾牌（兜底策略）。
    /// </summary>
    public IShield CreateShield()
    {
        return shieldFactory != null ? shieldFactory.CreateShield() : IShield.CreateDefault();
    }
}