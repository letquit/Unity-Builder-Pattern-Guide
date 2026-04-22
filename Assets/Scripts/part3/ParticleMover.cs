using UnityEngine;

/// <summary>
/// 粒子移动器。
/// 一个简单的直线移动组件，不依赖物理引擎。
/// 适用于简单的投射物或特效。
/// </summary>
public class ParticleMover : MonoBehaviour
{
    private float speed;          // 移动速度
    private Vector3 moveDirection; // 移动方向
    private bool isInitialized = false; // 初始化标志位，防止未配置时移动

    /// <summary>
    /// 初始化移动器。
    /// 设置速度并锁定初始方向为物体当前的朝向。
    /// </summary>
    /// <param name="speed">每秒移动的单位距离</param>
    public void Initialize(float speed)
    {
        this.speed = speed;
        // 锁定方向：使用物体当前的“前方”作为移动方向
        moveDirection = transform.forward; 
        isInitialized = true;
    }

    /// <summary>
    /// 每帧更新位置。
    /// </summary>
    private void Update()
    {
        // 守卫模式：如果未初始化，直接跳过
        if (!isInitialized) return;
        
        // 移动逻辑：位置 += 方向 * 速度 * 时间增量
        // 使用 Time.deltaTime 确保移动速度与帧率无关
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}