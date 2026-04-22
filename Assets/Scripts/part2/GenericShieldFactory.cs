using UnityEngine;

/// <summary>
/// 通用盾牌工厂。
/// 这是一个具体的工厂实现类，用于在编辑器中创建资产，并负责生产基础盾牌。
/// </summary>
[CreateAssetMenu(fileName = "GenericShieldFactory", menuName = "ShieldFactory/Generic")]
public class GenericShieldFactory : ShieldFactory
{
    /// <summary>
    /// 实现父类的抽象方法，返回一个基础盾牌实例。
    /// </summary>
    public override IShield CreateShield()
    {
        return new Shield();
    }
}