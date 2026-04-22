using DG.Tweening; // 引入 DOTween 插件，用于处理复杂的动画
using UnityEngine;

/// <summary>
/// 环绕法术策略。
/// 生成一组围绕施法者旋转的宝珠（Orbs）。
/// 适用于法师护甲、能量护盾等视觉效果。
/// </summary>
[CreateAssetMenu(fileName = "OrbitalSpellStrategy", menuName = "Spells/OrbitalSpellStrategy")]
public class OrbitalSpellStrategy : SpellStrategy
{
    // 宝珠预制体
    public GameObject orbPrefab;
    // 宝珠数量
    public int numberOfOrbs = 3;
    // 环绕半径
    public float radius = 3f;
    // 公转速度
    public float rotationSpeed = 0.3f;
    // 持续时间
    public float duration = 3f;
    // 生成高度偏移（相对于角色中心）
    public float spawnHeightOffset = 1f;
    // 垂直浮动幅度
    public float verticalMovementRange = 0.5f;

    /// <summary>
    /// 施法逻辑。
    /// 1. 创建父物体 -> 2. 旋转父物体 -> 3. 生成子物体 -> 4. 设定销毁时间
    /// </summary>
    /// <param name="origin">施法原点</param>
    public override void CastSpell(Transform origin)
    {
        // 1. 创建一个空的父物体，作为旋转中心，跟随角色
        Transform orbParent = CreateOrbParent(origin);
        
        // 2. 让这个父物体开始旋转（带动所有子物体公转）
        RotateOrbParent(orbParent);
        
        // 3. 生成指定数量的宝珠
        for (int i = 0; i < numberOfOrbs; i++)
        {
            SpawnOrb(origin, orbParent, i);
        }
        
        // 4. 持续时间结束后销毁整个父物体（包括所有宝珠）
        Destroy(orbParent.gameObject, duration);
    }

    /// <summary>
    /// 生成单个宝珠。
    /// </summary>
    private void SpawnOrb(Transform origin, Transform orbParent, int i)
    {
        // 计算初始位置并实例化
        var orb = Instantiate(orbPrefab, CalculateSpawnPosition(origin, i), Quaternion.identity, orbParent);
        
        // 添加垂直浮动动画
        MoveOrbVertically(orb, origin);
    }

    /// <summary>
    /// 计算宝珠在圆周上的位置。
    /// 使用三角函数将圆分成 N 等份。
    /// </summary>
    private Vector3 CalculateSpawnPosition(Transform origin, int i)
    {
        // 计算当前宝珠的角度（弧度）
        float angle = i * 2f * Mathf.PI / numberOfOrbs;
        
        // 计算 XZ 平面上的位置，并加上高度偏移
        return origin.position + new Vector3(Mathf.Cos(angle), spawnHeightOffset, Mathf.Sin(angle)) * radius;
    }

    /// <summary>
    /// 旋转父物体。
    /// 使用 DOTween 实现无限循环旋转。
    /// </summary>
    private void RotateOrbParent(Transform orbParent)
    {
        // 计算每秒旋转的角度
        float rotationRate = 360f * rotationSpeed;
        
        // DOTween.To 用于对非标准属性（如旋转）进行插值
        // 目标值：Y 轴每帧增加 rotationRate
        DOTween.To(() => orbParent.rotation.eulerAngles, x => orbParent.rotation = Quaternion.Euler(x),
                new Vector3(0, rotationRate, 0), 1f)
            .SetLoops(-1, LoopType.Incremental) // 无限循环，且数值累加（防止角度回弹）
            .SetEase(Ease.Linear);              // 匀速旋转
    }

    /// <summary>
    /// 创建并初始化父物体。
    /// </summary>
    private Transform CreateOrbParent(Transform origin)
    {
        var orbParent = new GameObject("OrbParent").transform;
        orbParent.position = origin.position;
        orbParent.rotation = origin.rotation;
        orbParent.SetParent(origin); // 设为子物体，跟随角色移动
        return orbParent;
    }

    /// <summary>
    /// 让宝珠上下浮动，并始终面朝外侧。
    /// </summary>
    private void MoveOrbVertically(GameObject orb, Transform origin)
    {
        // 随机浮动速度，让每个宝珠看起来更自然、不同步
        float verticalMovementDuration = Random.Range(0.2f, 0.5f);
        
        // DOMoveY：在 Y 轴上做往复运动（Yoyo）
        orb.transform.DOMoveY(orb.transform.position.y + verticalMovementRange, verticalMovementDuration)
            .SetLoops(-1, LoopType.Yoyo)
            // OnUpdate：每一帧动画更新时执行
            .OnUpdate(() =>
                // 计算朝向：从圆心指向宝珠的方向，并反射（面朝外）
                orb.transform.rotation = Quaternion.LookRotation(Vector3.Reflect(
                    orb.transform.position - origin.position, Vector3.up)
                ));
    }
}