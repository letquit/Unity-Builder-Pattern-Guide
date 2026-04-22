using UnityEngine;

/// <summary>
/// 弓箭工厂（单例模式）。
/// 与 SwordFactory 不同，这个工厂缓存了武器实例，确保全局只存在一个 Bow 对象。
/// 适用于共享武器或为了节省内存的场景。
/// </summary>
[CreateAssetMenu(fileName = "BowFactory", menuName = "WeaponFactory/Bow")]
public class BowFactory : WeaponFactory
{
    // 缓存的弓箭实例
    private IWeapon weapon;
    
    /// <summary>
    /// 创建武器。
    /// 实现了简单的单例模式：如果实例不存在则创建，存在则直接返回。
    /// </summary>
    public override IWeapon CreateWeapon()
    {
        if (weapon == null)
        {
            // 只有在第一次调用时才实例化
            weapon = new Bow();
        }
        // 返回缓存的实例
        return weapon;
    }
}