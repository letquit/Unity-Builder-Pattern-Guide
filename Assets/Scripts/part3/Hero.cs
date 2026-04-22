using System;
using UnityEngine;

/// <summary>
/// 英雄类。
/// 负责监听 UI 事件并执行相应的法术逻辑。
/// </summary>
public class Hero : MonoBehaviour
{
    // 法术策略数组：在编辑器中配置具体的法术（如火球术、冰箭术等）
    [SerializeField] private SpellStrategy[] spells;

    /// <summary>
    /// 当脚本启用时，订阅 UI 按钮点击事件。
    /// 注意：使用 += 进行订阅。
    /// </summary>
    private void OnEnable()
    {
        HeadsUpDisplay.OnButtonPressed += CastSpell;
    }
    
    /// <summary>
    /// 当脚本禁用时，注销事件监听。
    /// 注意：使用 -= 进行注销，防止内存泄漏或重复触发。
    /// </summary>
    private void OnDisable()
    {
        HeadsUpDisplay.OnButtonPressed -= CastSpell;
    }

    /// <summary>
    /// 释放法术。
    /// 根据 UI 传递的索引，调用对应法术策略的施法逻辑。
    /// </summary>
    /// <param name="index">法术数组的索引，对应 UI 按钮的索引</param>
    private void CastSpell(int index)
    {
        // 调用具体法术策略，并传入当前英雄的位置（transform）作为施法原点
        spells[index].CastSpell(transform);
    }
}