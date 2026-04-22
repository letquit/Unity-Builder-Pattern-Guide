using UnityEngine;

/// <summary>
/// 远程武器策略。
/// 具体的武器实现，定义了远程攻击（发射投射物）的行为。
/// </summary>
[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "Enemy/WeaponStrategy/Ranged")]
public class RangedWeaponStrategy : WeaponStrategy
{
    [SerializeField] private GameObject projectilePrefab; // 投射物预制体（如子弹、火球）
    [SerializeField] private Transform firePoint; // 发射点（如枪口、炮塔位置）
    
    /// <summary>
    /// 执行远程攻击。
    /// </summary>
    /// <param name="attacker">发起攻击的敌人</param>
    /// <param name="target">攻击目标</param>
    public override void Attack(Transform attacker, Transform target)
    {
        // 1. 检查目标是否在攻击范围内
        if (!IsInRange(attacker, target)) return;
        
        // 2. 确保投射物预制体已设置
        if (projectilePrefab != null)
        {
            // 3. 确定发射位置
            // 如果设置了 FirePoint，则使用 FirePoint；否则使用攻击者自身的位置
            Transform spawnPoint = firePoint != null ? firePoint : attacker;
            
            // 4. 计算发射方向
            Vector3 direction = (target.position - spawnPoint.position).normalized;
            
            // 5. 计算旋转角度，使投射物朝向目标
            Quaternion rotation = Quaternion.LookRotation(direction);
            
            // 6. 生成投射物
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, rotation);
            
            // 注意：这里通常需要进一步初始化投射物
            // 例如：projectile.GetComponent<Projectile>().SetDamage(Damage);
            
            Debug.Log($"{attacker.name} 远程攻击 {target.name}");
        }
    }
}