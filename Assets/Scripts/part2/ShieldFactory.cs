using UnityEngine;

/// <summary>
/// 盾牌接口。
/// 定义了所有盾牌必须具备的防御行为。
/// </summary>
public interface IShield
{
    // 防御/格挡行为
    void Defend();
    
    /// <summary>
    /// 静态工厂方法：创建默认盾牌。
    /// 提供基础实现，防止配置缺失时系统崩溃。
    /// </summary>
    static IShield CreateDefault()
    {
        return new Shield();
    }
}

/// <summary>
/// 基础盾牌类（具体实现）。
/// 实现了最普通的格挡逻辑。
/// </summary>
public class Shield : IShield
{
    public void Defend()
    {
        Debug.Log("Blocking with the shield!");
    }
}

/// <summary>
/// 盾牌工厂基类。
/// 继承自 ScriptableObject，允许在编辑器中创建资产来配置具体的盾牌生成逻辑。
/// </summary>
public abstract class ShieldFactory : ScriptableObject
{
    // 抽象方法：子类必须实现具体的创建逻辑（例如创建木盾、铁盾等）
    public abstract IShield CreateShield();
}