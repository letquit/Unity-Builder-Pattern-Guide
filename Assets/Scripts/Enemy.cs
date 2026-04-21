using UnityEngine;

/// <summary>
/// 敌人组件。
/// 代表游戏中的一个敌人实体。
/// </summary>
public class Enemy : MonoBehaviour
{
    // 敌人属性（只读，外部无法直接修改）
    public string Name { get; private set; }
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public bool IsBoss { get; private set; }
    
    // 这里可以添加敌人的行为逻辑，如移动、攻击等
    // Do Enemy Stuff
    
    /// <summary>
    /// 敌人建造者（内部类）。
    /// 使用链式调用简化敌人的创建过程。
    /// </summary>
    public class Builder
    {
        // 默认配置
        private string name = "Enemy";
        private int health = 100;
        private float speed = 5f;
        private int damage = 10;
        private bool isBoss = false;

        /// <summary>
        /// 设置敌人名称。
        /// </summary>
        public Builder WithName(string name)
        {
            this.name = name;
            return this; // 返回自身以支持链式调用
        }
    
        /// <summary>
        /// 设置生命值。
        /// </summary>
        public Builder WithHealth(int health)
        {
            this.health = health;
            return this;
        }

        /// <summary>
        /// 设置移动速度。
        /// </summary>
        public Builder WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
    
        /// <summary>
        /// 设置伤害值。
        /// </summary>
        public Builder WithDamage(int damage)
        {
            this.damage = damage;
            return this;
        }
    
        /// <summary>
        /// 设置是否为 Boss。
        /// </summary>
        public Builder WithIsBoss(bool isBoss)
        {
            this.isBoss = isBoss;
            return this;
        }
    
        /// <summary>
        /// 构建并返回最终的敌人对象。
        /// </summary>
        public Enemy Build()
        {
            // 1. 创建一个新的 GameObject 并挂载 Enemy 组件
            var enemy = new GameObject("Enemy").AddComponent<Enemy>();
            
            // 2. 将 Builder 中的数据赋值给组件属性
            enemy.Name = name;
            enemy.Health = health;
            enemy.Speed = speed;
            enemy.Damage = damage;
            enemy.IsBoss = isBoss;
            
            // 3. 返回构建好的敌人
            return enemy;
        }
    }
}