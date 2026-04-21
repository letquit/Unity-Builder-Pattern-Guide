using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

/// <summary>
/// 加密类型枚举。
/// DTLS: 用于 UDP 传输（PC, Mobile, Console）。
/// WSS: 用于 WebSocket 传输（WebGL）。
/// </summary>
[Serializable]
public enum EncryptionType
{
    DTLS,
    WSS
}

/// <summary>
/// 多人游戏服务管理器。
/// 负责协调 Relay（网络传输）和 Lobby（大厅匹配）服务，建立主机。
/// </summary>
public class MultiplayerService : MonoBehaviour
{
    [SerializeField] private string lobbyName = "Lobby"; // 大厅名称
    [SerializeField] private int maxPlayers = 4; // 最大玩家数
    [SerializeField] private EncryptionType encryption = EncryptionType.DTLS; // 加密方式

    private const string k_dtlsEncryption = "dtls";
    private const string k_wssEncryption = "wss";

    // 根据枚举返回对应的加密字符串
    private string ConnectionType => encryption == EncryptionType.DTLS ? k_dtlsEncryption : k_wssEncryption;

    private Lobby currentLobby; // 当前创建的大厅对象

    /// <summary>
    /// 创建大厅并启动主机。
    /// </summary>
    public async Task CreateLobby()
    {
        try
        {
            // 1. 创建 Relay 分配
            // maxPlayers - 1 是因为主机自己占用一个位置
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers - 1);

            // 2. 获取 Relay 的加入代码（短码）
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            // 3. 创建 Lobby 大厅
            var options = new CreateLobbyOptions { IsPrivate = false }; // 公开大厅
            currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            Debug.Log($"Created lobby: {currentLobby.Name}, code: {currentLobby.LobbyCode}, relay: {relayJoinCode}");

            // 4. 更新大厅数据
            // 将 Relay 的加入代码写入大厅数据，以便其他玩家获取
            await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, new UpdateLobbyOptionsBuilder()
                .AddMemberData("RelayJoinCode", relayJoinCode) // 成员可见：Relay 加入码
                .AddPrivateData("ExamplePrivateData", "PrivateData") // 私有数据：仅服务器可见
                .AddPublicData("ExamplePublicData", "PublicData", DataObject.IndexOptions.S1) // 公开数据：可被搜索
                .Build()
            );

            // 5. 配置 Unity Transport (UTP)
            var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();

            // 构建 Relay 服务器数据结构
            var relayServerData = new RelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                allocation.ConnectionData,
                encryption == EncryptionType.WSS // 是否为 WSS 模式
            );

            // 将 Relay 数据应用到传输组件
            utp.SetRelayServerData(relayServerData);
            
            // 6. 启动主机
            NetworkManager.Singleton.StartHost();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Failed to create lobby: " + e.Message);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Failed to setup relay: " + e.Message);
        }
    }
}