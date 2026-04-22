using UnityEngine;
using Utilities; // 引入 Utilities 命名空间，以便使用 Vector3Extensions

/// <summary>
/// 护盾法术策略。
/// 继承自 SpellStrategy，用于生成一个持续一定时间的护盾。
/// </summary>
[CreateAssetMenu(fileName = "ShieldSpellStrategy", menuName = "Spells/ShieldSpellStrategy")]
public class ShieldSpellStrategy : SpellStrategy
{
    // 护盾的预制体（Visual Model / Collider）
    public GameObject shieldPrefab;
    
    // 护盾持续时间（秒）
    public float duration = 10f;

    /// <summary>
    /// 施法逻辑。
    /// 在施法者位置生成护盾，并强制将其 Y 轴坐标归零（贴地）。
    /// </summary>
    /// <param name="origin">施法原点（通常是英雄的位置）</param>
    public override void CastSpell(Transform origin)
    {
        // 1. 实例化护盾
        // 2. 位置：使用扩展方法 .With(y: 0f) 锁定高度为 0，保持 X/Z 不变
        // 3. 旋转：无旋转
        // 4. 父级：设为施法者 (origin)，使护盾跟随角色移动
        var shield = Instantiate(shieldPrefab, origin.position.With(y: 0f), Quaternion.identity, origin);
        
        // 5. 在指定时间后销毁护盾
        Destroy(shield, duration);
    }
}