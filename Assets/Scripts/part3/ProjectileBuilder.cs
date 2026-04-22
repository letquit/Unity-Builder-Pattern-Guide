using UnityEngine;
using Utilities; // 引入命名空间以使用 Vector3Extensions 和 GameObjectExtensions

/// <summary>
/// 投射物建造者。
/// 使用建造者模式来配置和组装复杂的投射物 GameObject。
/// </summary>
public class ProjectileBuilder
{
    // 私有字段：存储配置数据
    private GameObject _projectilePrefab; // 预制体
    private float _speed;                 // 移动速度
    private float _duration;              // 存活时间

    /// <summary>
    /// 设置投射物预制体。
    /// </summary>
    public ProjectileBuilder WithProjectilePrefab(GameObject projectilePrefab)
    {
        _projectilePrefab = projectilePrefab;
        return this; // 返回自身，支持链式调用
    }

    /// <summary>
    /// 设置移动速度。
    /// </summary>
    public ProjectileBuilder WithSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
        
    /// <summary>
    /// 设置存活时间。
    /// </summary>
    public ProjectileBuilder WithDuration(float duration)
    {
        _duration = duration;
        return this;
    }
        
    /// <summary>
    /// 构建并返回最终的投射物 GameObject。
    /// 这一步负责将所有组件组装在一起并初始化。
    /// </summary>
    /// <param name="origin">发射源（如法师的手或法杖顶端）</param>
    /// <returns>构建完成的 GameObject</returns>
    public GameObject Build(Transform origin)
    {
        // 1. 计算生成位置：在发射源前方 2 个单位处生成，防止直接卡在模型里
        Vector3 instantPosition = origin.position + origin.forward * 2f;
            
        // 2. 实例化预制体
        // 使用 .With(y: 1) 强制将高度设为 1（例如腰部高度），避免从脚底射出
        GameObject fireball = Object.Instantiate(_projectilePrefab, instantPosition.With(y: 1), Quaternion.identity);
        
        // 3. 获取或添加必要组件
        // 即使预制体里没有这些组件，也能自动补全，保证功能正常
        ParticleMover mover = fireball.GetOrAdd<ParticleMover>();
        SelfDestruct selfDestruct = fireball.GetOrAdd<SelfDestruct>();
            
        // 4. 初始化组件数据
        mover.Initialize(_speed);       // 注入速度
        selfDestruct.Initialize(_duration); // 注入存活时间
            
        return fireball;
    }
}