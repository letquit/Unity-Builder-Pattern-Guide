using UnityEngine;
using System;

/// <summary>
/// 生命值组件。
/// 管理对象的健康状态，通过事件系统通知外部变化。
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // 最大生命值
    
    public int MaxHealth => maxHealth; // 只读最大生命值
    public int CurrentHealth { get; private set; } // 当前生命值（只读，外部只能读取）
    
    // 是否存活
    public bool IsAlive => CurrentHealth > 0;
    
    // 事件定义
    public event Action<int> OnHealthChanged; // 生命值变化时触发（参数：当前生命值）
    public event Action<int> OnDamageTaken;   // 受到伤害时触发（参数：实际伤害值）
    public event Action OnDeath;              // 死亡时触发
    public event Action OnHeal;               // 治疗时触发
    
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
    
    /// <summary>
    /// 受到伤害。
    /// </summary>
    /// <param name="damage">伤害数值</param>
    public void TakeDamage(int damage)
    {
        if (!IsAlive) return; // 如果已死亡，忽略伤害
        if (damage <= 0) return; // 无效伤害忽略
        
        int previousHealth = CurrentHealth;
        // 确保生命值不低于 0
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        // 计算实际受到的伤害（处理溢出伤害）
        int actualDamage = previousHealth - CurrentHealth;
        
        // 触发事件
        OnHealthChanged?.Invoke(CurrentHealth);
        OnDamageTaken?.Invoke(actualDamage);
        
        Debug.Log($"{name} 受到 {actualDamage} 伤害, 剩余生命: {CurrentHealth}/{maxHealth}");
        
        if (!IsAlive)
        {
            Die();
        }
    }
    
    /// <summary>
    /// 恢复生命。
    /// </summary>
    /// <param name="amount">治疗数值</param>
    public void Heal(int amount)
    {
        if (!IsAlive) return; // 死亡单位无法治疗
        if (amount <= 0) return; // 无效治疗忽略
        
        int previousHealth = CurrentHealth;
        // 确保生命值不超过最大值
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        int actualHeal = CurrentHealth - previousHealth;
        
        if (actualHeal > 0)
        {
            OnHealthChanged?.Invoke(CurrentHealth);
            OnHeal?.Invoke();
            Debug.Log($"{name} 恢复 {actualHeal} 生命, 当前: {CurrentHealth}/{maxHealth}");
        }
    }
    
    /// <summary>
    /// 设置最大生命值并回满血。
    /// </summary>
    /// <param name="health">新的最大生命值</param>
    public void SetMaxHealth(int health)
    {
        maxHealth = Mathf.Max(1, health); // 至少为 1
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
    
    /// <summary>
    /// 死亡逻辑。
    /// </summary>
    private void Die()
    {
        Debug.Log($"{name} 已死亡");
        OnDeath?.Invoke();
        
        // 可以在这里添加自动销毁或通知游戏管理器的逻辑
        // Destroy(gameObject);
    }
    
    /// <summary>
    /// 重置生命值为满。
    /// </summary>
    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}