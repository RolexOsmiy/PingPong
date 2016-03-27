using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour 
{
    private static MultiplayerManager _instance;
    public static MultiplayerManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject[] GameUI;
    private bool isConnected = false;

    private string MatchName = "";
    private string MatchPassword = "";

    public string playerName = "";

    public class PlayerInfo
    {
        public string Name = "";
        public NetworkPlayer PlayerNetwork;
    }

    public List<PlayerInfo> PlayerList = new List<PlayerInfo>();
    public PlayerInfo MyPlayer;


    public GameObject PlayerPrefab;
    public GameObject BallPrefab;

    private bool showServerList = false;

    public void Start()
    {
        _instance = this;
    }

    public void FixedUpdate()
    {
        if (!isConnected)
        {
            for (int i = 0; i < GameUI.Length; i++)
            {
                GameUI[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < GameUI.Length; i++)
            {
                GameUI[i].SetActive(true);
            }
        }
    }

    public void StartServer(string servername, string serverpassword)
    {
        if (playerName != "")
        {
            MatchName = servername;
            MatchPassword = serverpassword;
            Network.InitializeServer(2, 7777, false);
            Network.incomingPassword = serverpassword;
            //Network.InitializeSecurity();
            MasterServer.RegisterHost("PingPongMatch", MatchName, "");
        }
        else
        {
            Console.Write("Set player name first!");
        }
    }

    public void ConnectToServer(string iptemp, int port, string password)
    {
        if (playerName != "")
        {
            Network.Connect(iptemp, port, password);
        }
        else
        {
            Console.Write("Set player name first!");
        }
    }

    public void DisconnectFromServer()
    {
        Network.Disconnect();
    }

    public void SetPlayerName(string name)
    {
        if (name != "")
        {
            playerName = name;
            Console.Write("Name successfuly established or chanched to <color=blue>" + name + "</color>");
        }
        else
        {
            Console.Write("<color=red>Error to set a name</color>");
        }
    }

    public void Console_ShowPlayerList()
    {
        if (isConnected)
        {
            Console.Write("===================================");
            foreach (PlayerInfo pl in PlayerList)
            {
                Console.Write("<color=green>" + pl.Name + "</color>");
            }
            Console.Write("===================================");
        }
        else
        {
            Console.Write("<color=red>Don't connected on any server!</color>");
        }
    }



    public void ShowServerList()
    {
        showServerList = true;
    }

    public void OnGUI()
    {
        if (showServerList)
        {
            GUILayout.BeginArea(new Rect(10,10, 300, 300), "Server List", "Window");
            scrolPos = GUILayout.BeginScrollView(scrolPos);
            foreach (HostData match in MasterServer.PollHostList())
            {
                GUILayout.BeginHorizontal("Box");

                GUILayout.Label(match.gameName);
                GUILayout.Label(match.connectedPlayers + "/" + match.playerLimit);
                if (GUILayout.Button("Connect"))
                {
                    if (match.connectedPlayers < match.playerLimit)
                    {
                        Network.Connect(match);
                    }
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();


            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Refresh"))
            {
                MasterServer.RequestHostList("PingPongMatch");
            }
            if (GUILayout.Button("Close"))
            {
                showServerList = false;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    Vector2 scrolPos = Vector2.zero;




    void OnServerInitialized()
    {
        Console.Write("Server Started!");
        isConnected = true;
        Server_PlayerJoinRequest(playerName, Network.player);
    }

    [RPC]
    void Server_PlayerJoinRequest(string PlayerName, NetworkPlayer view)
    {
        GetComponent<NetworkView>().RPC("Client_AddPlayerToList", RPCMode.All, PlayerName, view);
    }

    [RPC]
    void Client_AddPlayerToList(string Name, NetworkPlayer view)
    {
        PlayerInfo plInfo = new PlayerInfo();
        plInfo.Name = Name;
        plInfo.PlayerNetwork = view;
        PlayerList.Add(plInfo);
        if (Network.player == view)
        {
            MyPlayer = plInfo;
        }
        if (PlayerList.Count == 2)
        {
            StartMatch();
        }
        Console.Write("Player joined and added to Player List");
    }

    void StartMatch()
    {
        Console.Write("Match Started!");
        Console.Write("/close");


    }

    void OnConnectedToServer()
    {
        Console.Write("Connected To Server");
        isConnected = true;
    }

    void OnFailedConnectToServer()
    {

    }

    void OnDisconnectedFromServer()
    {
        Console.Write("Disconnected!");
        isConnected = false;
    }


}