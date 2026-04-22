using UnityEngine;

/// <summary>
/// 投射物法术策略。
/// 负责定义一个发射投射物的法术（如火球、箭矢）。
/// 它利用 ProjectileBuilder 来封装复杂的构建逻辑。
/// </summary>
[CreateAssetMenu(fileName = "ProjectileSpellStrategy", menuName = "Spells/ProjectileSpellStrategy")]
public class ProjectileSpellStrategy : SpellStrategy
{
    // 投射物预制体（视觉模型）
    public GameObject projectilePrefab;
    
    // 飞行速度
    public float speed = 10f;
    
    // 存活时间（决定了射程）
    public float duration = 10f;
    
    /// <summary>
    /// 施法逻辑。
    /// 使用建造者模式来组装和发射投射物。
    /// </summary>
    /// <param name="origin">施法原点（如法师的手或法杖）</param>
    public override void CastSpell(Transform origin)
    {
        // 链式调用：配置 -> 构建
        // 这种方式让代码读起来非常像自然语言，且易于维护
        new ProjectileBuilder()
            .WithProjectilePrefab(projectilePrefab) // 1. 设置预制体
            .WithSpeed(speed)                       // 2. 设置速度
            .WithDuration(duration)                 // 3. 设置持续时间
            .Build(origin);                         // 4. 执行构建并生成物体
    }
}