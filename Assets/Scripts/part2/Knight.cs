using System;
using Sirenix.OdinInspector; // 引入 Odin Inspector 库，用于增强编辑器功能
using UnityEngine;

/// <summary>
/// 骑士类。
/// 演示了如何通过工厂模式解耦武器创建逻辑。
/// </summary>
public class Knight : MonoBehaviour
{
    // [Required] 特性：在编辑器中如果该字段为空，会显示错误提示，防止运行时空引用
    [SerializeField, Required] private WeaponFactory weaponFactory;
    
    // 当前持有的武器接口
    // 初始化为默认值，防止未赋值时调用报错
    private IWeapon weapon = IWeapon.CreateDefault();

    private void Start()
    {
        // 如果工厂存在，则通过工厂创建具体的武器实例
        if (weaponFactory != null)
        {
            weapon = weaponFactory.CreateWeapon();
        }
        
        // 执行攻击动作
        Attack();
    }
    
    /// <summary>
    /// 攻击方法。
    /// 调用当前武器的攻击逻辑。
    /// 使用 ?. 操作符确保 weapon 不为空时才调用，避免崩溃。
    /// </summary>
    public void Attack() => weapon?.Attack();
}