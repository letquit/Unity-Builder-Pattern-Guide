using UnityEngine;

namespace Utilities {
    /// <summary>
    /// Vector3 扩展类。
    /// 提供便捷的链式方法来修改或操作 Vector3 的分量。
    /// </summary>
    public static class Vector3Extensions {
        
        /// <summary>
        /// 设置 Vector3 的任意值。
        /// 如果参数为 null，则保持原值不变。
        /// 这是一个非常有用的“语法糖”，避免了 new Vector3(v.x, newY, v.z) 这种冗长的写法。
        /// </summary>
        /// <param name="vector">原始向量</param>
        /// <param name="x">新的 X 值（可选）</param>
        /// <param name="y">新的 Y 值（可选）</param>
        /// <param name="z">新的 Z 值（可选）</param>
        /// <returns>一个新的 Vector3 实例</returns>
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
            // 使用空合并运算符：如果传入值则用传入值，否则用原向量的值
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
    
        /// <summary>
        /// 向 Vector3 的任意值添加增量。
        /// 如果参数为 null，则该轴不增加。
        /// </summary>
        /// <param name="vector">原始向量</param>
        /// <param name="x">X 轴的增量（可选）</param>
        /// <param name="y">Y 轴的增量（可选）</param>
        /// <param name="z">Z 轴的增量（可选）</param>
        /// <returns>一个新的 Vector3 实例</returns>
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
            // 使用空合并运算符：如果传入值则加上该值，否则加 0
            return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }
    }
}