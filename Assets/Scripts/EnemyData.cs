using UnityEngine;

/// <summary>
/// 敌人数据配置。
/// 使用 ScriptableObject 存储敌人的属性数据，实现数据驱动设计。
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/Data")] // 在编辑器中右键 -> Create -> Enemy -> Data 即可创建此资产
public class EnemyData : ScriptableObject
{
    // 武器策略引用。
    // 注意：ScriptableObject 通常不支持直接序列化多态类型，
    // 这里可能需要配合具体的引用或自定义序列化逻辑使用。
    public WeaponStrategy WeaponStrategy { get; set; }
    
    public int MaxHealth { get; set; } = 100; // 最大生命值
    public float MoveSpeed { get; set; } = 5f; // 移动速度
    public int Damage { get; set; } = 10; // 基础伤害
    public bool IsBoss { get; set; } = false; // 是否为 Boss 标记
    
    [Header("Visual Settings")] // 在 Inspector 中显示为分组标题
    public Color BodyColor = Color.red; // 身体颜色（用于程序化生成材质等）
    public float SizeMultiplier = 1.0f; // 大小缩放倍率
}