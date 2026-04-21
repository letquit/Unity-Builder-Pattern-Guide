using UnityEngine;

/// <summary>
/// 敌人建造者接口（起始阶段）。
/// 定义构建的第一步：添加武器组件。
/// </summary>
public interface IEnemyBuilder
{
    IWeaponEnemyBuilder AddWeaponComponent();
}

/// <summary>
/// 武器建造者接口（第二阶段）。
/// 定义构建的第二步：设置武器策略。
/// 注意：只有完成了第一步（添加组件），才能进入这一步（设置策略）。
/// </summary>
public interface IWeaponEnemyBuilder
{
    IHealthEnemyBuilder AddWeaponStrategy(WeaponStrategy strategy);
}

/// <summary>
/// 生命值建造者接口（第三阶段）。
/// 定义构建的第三步：添加生命值组件。
/// </summary>
public interface IHealthEnemyBuilder
{
    IFinalEnemyBuilder AddHealthComponent();
}

/// <summary>
/// 最终建造者接口（结束阶段）。
/// 定义构建的最后一步：构建并返回最终对象。
/// </summary>
public interface IFinalEnemyBuilder
{
    Enemy Build();
}

/// <summary>
/// 敌人指挥者。
/// 负责定义构建敌人的顺序，但不关心具体实现细节。
/// </summary>
public class EnemyDirector
{
    private IEnemyBuilder builder;

    public EnemyDirector(IEnemyBuilder builder)
    {
        this.builder = builder;
    }
    
    /// <summary>
    /// 构建敌人。
    /// 按照严格的顺序调用建造者的方法。
    /// </summary>
    // public Enemy Construct(EnemyBuilder builder, EnemyData data) // 旧版本：每次传入建造者
    public Enemy Construct(EnemyData data) // 新版本：建造者在构造函数中注入
    {
        return builder
            .AddWeaponComponent()      // 1. 添加武器组件
            .AddWeaponStrategy(data.WeaponStrategy) // 2. 配置武器策略（使用数据）
            .AddHealthComponent()      // 3. 添加生命值组件
            .Build();                  // 4. 完成构建
    }
}

/// <summary>
/// 具体的敌人建造者。
/// 实现了所有建造阶段接口，负责实际的组件组装。
/// </summary>
public class EnemyBuilder : IEnemyBuilder, IWeaponEnemyBuilder, IHealthEnemyBuilder, IFinalEnemyBuilder
{
    // 当前正在构建的敌人对象
    private Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();

    /// <summary>
    /// 步骤 1：添加武器组件。
    /// </summary>
    public IWeaponEnemyBuilder AddWeaponComponent()
    {
        enemy.gameObject.AddComponent<EnemyWeapon>();
        return this; // 返回下一个阶段的接口
    }

    /// <summary>
    /// 步骤 2：设置武器策略。
    /// </summary>
    public IHealthEnemyBuilder AddWeaponStrategy(WeaponStrategy strategy)
    {
        // 获取刚才添加的武器组件并设置策略
        if (enemy.gameObject.TryGetComponent<EnemyWeapon>(out var weapon))
        {
            weapon.SetWeaponStrategy(strategy);
        }
        
        return this; // 返回下一个阶段的接口
    }
    
    /// <summary>
    /// 步骤 3：添加生命值组件。
    /// </summary>
    public IFinalEnemyBuilder AddHealthComponent()
    {
        enemy.gameObject.AddComponent<Health>();
        return this; // 返回最后一个阶段的接口
    }

    /// <summary>
    /// 步骤 4：完成构建并返回对象。
    /// 同时重置建造者以便复用。
    /// </summary>
    public Enemy Build()
    {
        Enemy builtEnemy = enemy; // 保存当前构建好的敌人
        
        // 重置：创建一个新的空敌人对象，供下一次构建使用
        enemy = new GameObject("Enemy").AddComponent<Enemy>();
        
        return builtEnemy;
    }
}