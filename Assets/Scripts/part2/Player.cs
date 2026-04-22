using UnityEngine;

/// <summary>
/// 网络玩家类。
/// 使用私有构造函数和静态工厂方法，强制要求玩家在创建时必须注册到 GameManager。
/// </summary>
public class NetworkPlayer {
    // 玩家名称，只读，外部无法直接修改
    public string Name { get; private set; }
    
    // 玩家生命值，只读
    public int Health { get; private set; }

    /// <summary>
    /// 私有构造函数。
    /// 禁止外部直接使用 new NetworkPlayer()，强制通过工厂方法创建。
    /// </summary>
    private NetworkPlayer(string name, int health)
    {
        Name = name;
        Health = health;
    }

    /// <summary>
    /// 静态工厂方法：创建并注册玩家。
    /// 这是一个“原子操作”，确保玩家被创建的同时一定被系统记录。
    /// </summary>
    /// <param name="name">玩家名称</param>
    /// <param name="health">初始生命值</param>
    /// <returns>新创建的玩家实例</returns>
    public static NetworkPlayer CreateAndRegister(string name, int health)
    {
        // 1. 创建实例
        NetworkPlayer player = new NetworkPlayer(name, health);
        
        // 2. 自动注册到全局管理器
        GameManager.RegisterPlayer(player);
        
        return player;
    }
}