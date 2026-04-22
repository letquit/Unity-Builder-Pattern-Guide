using System;
using UnityEngine;

/// <summary>
/// 士兵类。
/// 一个通用的战斗单位，通过依赖注入的方式从外部工厂获取装备。
/// </summary>
public class Soldier : MonoBehaviour
{
    // 装备工厂：决定了这个士兵使用什么武器和盾牌
    [SerializeField] private EquipmentFactory equipmentFactory;

    // --- 传统写法（已注释） ---
    // 在 Start 中缓存引用，避免重复创建
    // private IWeapon weapon;
    // private IShield shield;
    //
    // private void Start()
    // {
    //     weapon = equipmentFactory.CreateWeapon();
    //     shield = equipmentFactory.CreateShield();
    //     
    //     Attack();
    //     Defend();
    // }
    
    /// <summary>
    /// 攻击方法。
    /// 每次调用都向工厂请求武器。
    /// 注意：如果工厂没有缓存（如 BowFactory），这会导致每次都 new 一个新对象，产生垃圾回收压力。
    /// </summary>
    public void Attack() => equipmentFactory.CreateWeapon().Attack();

    /// <summary>
    /// 防御方法。
    /// 每次调用都向工厂请求盾牌。
    /// </summary>
    public void Defend() => equipmentFactory.CreateShield().Defend();
    
    // 提示：建议让工厂内部缓存默认值（单例模式），以避免每次调用 CreateWeapon 都创建新实例。
}