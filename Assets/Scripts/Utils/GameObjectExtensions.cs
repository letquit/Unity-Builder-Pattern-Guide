using UnityEngine;

namespace Utilities {
    /// <summary>
    /// GameObject 扩展类。
    /// 提供便捷的方法来操作 GameObject 及其组件。
    /// </summary>
    public static class GameObjectExtensions {
        
        /// <summary>
        /// 获取或添加组件。
        /// 如果 GameObject 上已经存在组件 T，则返回该组件；
        /// 如果不存在，则添加组件 T 并返回新添加的实例。
        /// 这是一个非常常用的“懒汉式”工具方法，避免了空引用检查的繁琐代码。
        /// </summary>
        /// <typeparam name="T">需要获取或添加的组件类型</typeparam>
        /// <param name="gameObject">目标 GameObject</param>
        /// <returns>组件 T 的引用</returns>
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component {
            // 尝试获取已存在的组件
            T component = gameObject.GetComponent<T>();
            
            // 如果存在则返回，不存在则添加
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }
}