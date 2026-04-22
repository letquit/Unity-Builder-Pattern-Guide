using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

/// <summary>
/// 大厅选项更新构建器。
/// 使用建造者模式简化 Unity Lobby Service 的数据更新配置。
/// </summary>
public class UpdateLobbyOptionsBuilder
{
    // 内部存储待更新的数据字典
    private readonly Dictionary<string, DataObject> data = new();

    /// <summary>
    /// 添加公开数据。
    /// 公开数据可被大厅列表搜索到（例如：游戏模式、地图名称）。
    /// </summary>
    /// <param name="key">数据键</param>
    /// <param name="value">数据值</param>
    /// <param name="index">索引选项，用于配置搜索过滤</param>
    /// <returns>返回当前构建器实例，支持链式调用</returns>
    public UpdateLobbyOptionsBuilder AddPublicData(string key, string value, DataObject.IndexOptions index = default)
    {
        // 创建可见性为 Public 的数据对象
        data.Add(key, new DataObject(DataObject.VisibilityOptions.Public, value, index));
        return this;
    }

    /// <summary>
    /// 添加成员数据。
    /// 仅大厅内的成员可见的数据。
    /// </summary>
    /// <param name="key">数据键</param>
    /// <param name="value">数据值</param>
    /// <param name="index">索引选项</param>
    /// <returns>返回当前构建器实例，支持链式调用</returns>
    public UpdateLobbyOptionsBuilder AddMemberData(string key, string value, DataObject.IndexOptions index = default)
    {
        data.Add(key, new DataObject(DataObject.VisibilityOptions.Member, value, index));
        return this;
    }
    
    /// <summary>
    /// 添加私有数据。
    /// 私有数据不可被搜索，通常用于服务器端逻辑（例如：房间密码、随机种子）。
    /// </summary>
    /// <param name="key">数据键</param>
    /// <param name="value">数据值</param>
    /// <param name="index">索引选项</param>
    /// <returns>返回当前构建器实例，支持链式调用</returns>
    public UpdateLobbyOptionsBuilder AddPrivateData(string key, string value, DataObject.IndexOptions index = default)
    {
        data.Add(key, new DataObject(DataObject.VisibilityOptions.Private, value, index));
        return this;
    }

    /// <summary>
    /// 构建最终的 UpdateLobbyOptions 对象。
    /// </summary>
    /// <returns>包含所有配置数据的选项对象</returns>
    public UpdateLobbyOptions Build()
    {
        return new UpdateLobbyOptions
        {
            Data = data
        };
    }
}