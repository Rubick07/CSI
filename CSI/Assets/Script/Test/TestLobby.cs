using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;

public class TestLobby : MonoBehaviour
{
    [SerializeField] TMP_Text _Text, NameText, LobbyType;
    private Lobby hostLobby;
    private Lobby JoinedLobby;
    private float HeartbeatTimer;
    private float lobbyUpdateTimer;
    private string PlayerName;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In" + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        PlayerName = "Brody" + UnityEngine.Random.Range(10, 99);
        NameText.text = PlayerName;
        Debug.Log(PlayerName);
    }

    private void Update()
    {
        HandleLobbyTimer();
        HandleLobbyPollForUpdates();
    }

    private async void HandleLobbyTimer()
    {
        if(hostLobby != null)
        {
            HeartbeatTimer -= Time.deltaTime;
            if(HeartbeatTimer < 0f)
            {
                float HeartBeatMax = 15f;
                HeartbeatTimer = HeartBeatMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }

        }

    }

    private async void HandleLobbyPollForUpdates()
    {
        if (JoinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0f)
            {
                float lobbyUpdateMax = 1.1f;
                lobbyUpdateTimer = lobbyUpdateMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(JoinedLobby.Id);
                JoinedLobby = lobby;
                LobbyType.text = JoinedLobby.Data["GameMode"].Value;
            }

        }
    }



    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int MaxPlayer = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Casual") }
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, MaxPlayer, createLobbyOptions);

            hostLobby = lobby;
            JoinedLobby = hostLobby;
            _Text.text = lobby.LobbyCode;


            Debug.Log("Created Lobby" + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);
            PrintPlayers(hostLobby);
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    public async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies Found" + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers + lobby.Data["GameMode"].Value);
            }
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    public async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            
            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }

    public async void JoinLobbyByCode(string LobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer(),
            };

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(LobbyCode, joinLobbyByCodeOptions);
            JoinedLobby = lobby;
            _Text.text = LobbyCode;
            Debug.Log("Join Lobby With Code" + LobbyCode);
            PrintPlayers(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
       
    }

    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(JoinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }

    public void PrintPlayers()
    {
        PrintPlayers(JoinedLobby);
    }

    public void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in Lobby " + lobby.Name + " " + lobby.Data["GameMode"].Value);
        foreach(Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
               {
               {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerName)}
               }
        };
    }

    public async void UpdateLobbyGameMode(string gameMode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions {
                Data = new Dictionary<string, DataObject> {
                {"GameMode",new DataObject(DataObject.VisibilityOptions.Public, gameMode)  } }
                }
            );
            JoinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    public async void UpdatePlayerName(string NewPlayerName)
    {
        try
        {
            NameText.text = NewPlayerName;
            PlayerName = NewPlayerName;
            await LobbyService.Instance.UpdatePlayerAsync(JoinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerName)}
                }
            });

        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    private async void MigrateLobbyHost()
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                HostId = JoinedLobby.Players[1].Id
            }
            );
            JoinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

}
