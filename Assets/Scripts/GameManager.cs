using System;
using UnityEngine;

/// <summary>
/// 游戏管理器。
/// 负责管理游戏核心逻辑，此处演示如何使用建造者模式创建敌人。
/// </summary>
public class GameManager : MonoBehaviour
{
    // 敌人配置数据：在编辑器中可配置的 ScriptableObject 资产
    [SerializeField] private EnemyData enemyData = default; 
    
    // 敌人构建指挥者：负责控制构建流程
    private EnemyDirector enemyDirector; 
    
    private void Start()
    {
        // --- 以下是几种不同的构建方式演示（已注释） ---

        // 1. 简单的链式调用（不使用指挥者）
        // 适用于简单对象，直接在代码中硬编码参数。
        // Enemy enemy = new Enemy.Builder()
        //     .WithName("Enemy")
        //     .WithHealth(100)
        //     .WithSpeed(10)
        //     .WithDamage(10)
        //     .Build();
        
        // 2. 使用指挥者，但每次手动传入建造者
        // 这种方式比较繁琐，每次都要 new 一个建造者。
        // Enemy enemy = new EnemyDirector().Construct(new EnemyBuilder(), enemyData);
        
        // 3. 将指挥者作为字段，复用实例
        // 稍微好一点，但建造者还是每次都要创建。
        // enemyDirector = new EnemyDirector();
        // Enemy enemy = enemyDirector.Construct(new EnemyBuilder(), enemyData);
        
        // --- 当前使用的最佳实践 ---
        
        // 初始化指挥者，并注入具体的建造者
        // 这样 GameManager 只需要关心“构建什么”，不需要关心“怎么构建”
        // 这里的 new EnemyBuilder() 也可以改为通过依赖注入获取
        enemyDirector = new EnemyDirector(new EnemyBuilder());
        
        // 使用指挥者根据数据构建敌人
        // 这里的 enemyData 包含了所有必要的配置信息（名字、血量、攻击力等）
        Enemy enemy = enemyDirector.Construct(enemyData);

        // 将构建好的敌人实例化到场景中
        Instantiate(enemy);
        
        // 注意：在生产环境中，通常会使用对象池来管理实例，而不是直接 Instantiate
        // or put in object pool, etc.
    }

    /// <summary>
    /// 注册玩家（静态方法示例）。
    /// </summary>
    public static void RegisterPlayer(NetworkPlayer player)
    {
        Debug.Log($"Registered player: {player.Name}, Health: {player.Health}");
    }
}