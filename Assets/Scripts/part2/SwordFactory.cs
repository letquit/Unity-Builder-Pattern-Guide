using UnityEngine;

/// <summary>
/// 长剑工厂。
/// 具体的武器工厂实现，专门用于生产 Sword（剑）类型的武器。
/// 可通过编辑器菜单创建资产，实现数据驱动。
/// </summary>
[CreateAssetMenu(fileName = "SwordFactory", menuName = "WeaponFactory/Sword")]
public class SwordFactory : WeaponFactory
{
    /// <summary>
    /// 实现父类抽象方法，返回一个新的剑实例。
    /// </summary>
    public override IWeapon CreateWeapon()
    {
        return new Sword();
    }
}