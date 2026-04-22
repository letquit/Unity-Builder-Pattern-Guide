using UnityEngine;

/// <summary>
/// 武器接口。
/// 定义了所有武器必须具备的行为。
/// </summary>
public interface IWeapon
{
    // 攻击行为
    void Attack();

    /// <summary>
    /// 静态工厂方法：创建默认武器。
    /// 这是一种“保底”策略，确保即使没有外部工厂，系统也能生成一个基础武器（剑）。
    /// </summary>
    static IWeapon CreateDefault()
    {
        return new Sword();
    }
}

/// <summary>
/// 剑类（具体武器实现）。
/// 实现了近战挥砍逻辑。
/// </summary>
public class Sword : IWeapon
{
    public void Attack()
    {
        Debug.Log("Swing the sword!");
    }
}

/// <summary>
/// 弓类（具体武器实现）。
/// 实现了远程射击逻辑。
/// </summary>
public class Bow : IWeapon
{
    public void Attack()
    {
        Debug.Log("Shoot an arrow!");
    }
}

/// <summary>
/// 武器工厂基类。
/// 继承自 ScriptableObject，允许在 Unity 编辑器中创建资产文件。
/// 这使得我们可以通过配置而非代码来指定生成哪种武器。
/// </summary>
public abstract class WeaponFactory : ScriptableObject
{
    // 抽象方法：子类必须实现具体的创建逻辑
    public abstract IWeapon CreateWeapon();
}