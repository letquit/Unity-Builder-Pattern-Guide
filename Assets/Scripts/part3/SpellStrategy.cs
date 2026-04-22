using UnityEngine;

/// <summary>
/// 法术策略基类。
/// 继承自 ScriptableObject，允许在编辑器中创建法术资产。
/// 这是策略模式的核心，定义了所有法术必须遵循的标准。
/// </summary>
public abstract class SpellStrategy : ScriptableObject
{
    /// <summary>
    /// 施法抽象方法。
    /// 所有具体的法术（子类）都必须实现这个方法来定义具体的施法逻辑。
    /// </summary>
    /// <param name="origin">施法的原点（通常是英雄的位置和旋转），用于确定法术生成的位置或方向</param>
    public abstract void CastSpell(Transform origin);
}