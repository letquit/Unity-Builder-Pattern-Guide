using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 平视显示器（HUD）。
/// 负责管理界面上的按钮，并将点击事件转换为全局事件。
/// </summary>
public class HeadsUpDisplay : MonoBehaviour
{
    // 按钮数组：在编辑器中将 UI 按钮拖拽赋值到这里
    [SerializeField] private Button[] buttons;

    // 定义委托：用于声明事件的数据签名
    // 这里表示事件会传递一个 int 类型的参数（按钮的索引）
    public delegate void ButtonPressedEvent(int index);

    // 静态事件：允许其他脚本订阅按钮点击，但不需要持有本脚本的引用
    // 这是一个典型的观察者模式实现
    public static event ButtonPressedEvent OnButtonPressed;

    private void Awake()
    {
        // 遍历所有按钮，动态添加点击监听
        for (int i = 0; i < buttons.Length; i++)
        {
            // 【关键点】：捕获循环变量的当前值
            // 如果不写这一行，直接在 Lambda 中使用 i，
            // 所有按钮点击时都会得到同一个值（即循环结束后的 i 值）。
            int index = i;
            
            // 添加监听器，使用 Lambda 表达式调用处理方法
            buttons[i].onClick.AddListener(() => HandleButtonPressed(index));
        }
    }
    
    /// <summary>
    /// 处理按钮点击。
    /// 触发静态事件，通知所有订阅者哪个按钮被按下了。
    /// 使用 ?. 操作符防止没有订阅者时报错。
    /// </summary>
    /// <param name="index">被点击按钮的索引</param>
    private void HandleButtonPressed(int index) => OnButtonPressed?.Invoke(index);
}