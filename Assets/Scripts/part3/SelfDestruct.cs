using UnityEngine;

/// <summary>
/// 自毁组件。
/// 用于在指定时间后自动销毁挂载该脚本的游戏物体。
/// 常用于子弹、特效、临时生成的物体等。
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    /// <summary>
    /// 初始化自毁计时器。
    /// </summary>
    /// <param name="lifetime">存活时间（秒）。在此时间过后，物体会被销毁。</param>
    public void Initialize(float lifetime)
    {
        // Unity 内置的延迟销毁方法
        // 第一个参数是目标物体（这里指当前物体），第二个参数是延迟时间
        Destroy(gameObject, lifetime);
    }
}