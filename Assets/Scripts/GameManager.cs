using System;
using UnityEngine;

/// <summary>
/// 游戏管理器。
/// 负责管理游戏核心逻辑，此处演示如何使用建造者模式创建敌人。
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = default; // 敌人配置数据（ScriptableObject）
    
    private EnemyDirector enemyDirector; // 敌人构建指挥者
    
    private void Start()
    {
        // --- 以下是几种不同的构建方式演示 ---

        // 1. 简单的链式调用（不使用指挥者）
        // Enemy enemy = new Enemy.Builder()
        //     .WithName("Enemy")
        //     .WithHealth(100)
        //     .WithSpeed(10)
        //     .WithDamage(10)
        //     .Build();
        
        // 2. 使用指挥者，但每次手动传入建造者
        // Enemy enemy = new EnemyDirector().Construct(new EnemyBuilder(), enemyData);
        
        // 3. 将指挥者作为字段，复用实例
        // enemyDirector = new EnemyDirector();
        // Enemy enemy = enemyDirector.Construct(new EnemyBuilder(), enemyData);
        
        // --- 当前使用的最佳实践 ---
        
        // 初始化指挥者，并注入具体的建造者
        // 这样 GameManager 只需要关心“构建什么”，不需要关心“怎么构建”
        enemyDirector = new EnemyDirector(new EnemyBuilder());
        
        // 使用指挥者根据数据构建敌人
        // 这里的 enemyData 包含了所有必要的配置信息
        Enemy enemy = enemyDirector.Construct(enemyData);

        // 将构建好的敌人实例化到场景中
        Instantiate(enemy);
        
        // 注意：在生产环境中，通常会使用对象池来管理实例，而不是直接 Instantiate
        // or put in object pool, etc.
    }
}