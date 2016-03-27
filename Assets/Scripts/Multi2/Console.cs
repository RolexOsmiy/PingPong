using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour 
{
    public GUISkin mySkin;
    public static List<string> messages = new List<string>();
    public static bool showConsole = false;
    public Rect windowRect = new Rect(100, 100, 600, 600);
    private Vector2 scrolpos = Vector2.zero;
    public string message = "";
    /// <summary>
    /// Записывать лог на уровне Unity. т.е. использовать Debug.Log() при записи в консоль
    /// </summary>
    public static bool DebugOnUnityLevel = false;
    public static bool CheckForInput = true;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Write("Welcome to PingPong console!! \n"+"Type [<color=green>/help</color>] to show all allow codes.");
    }

    public static void Write(string what)
    {
        messages.Add(what);
        if (DebugOnUnityLevel)
            Debug.Log(what);
    }

    public void Update()
    {
        if (CheckForInput)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !showConsole)
            {
                showConsole = true;
                GUI.FocusWindow(0);
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && showConsole)
            {
                showConsole = false;
            }
        }
        //scrolpos = Vector2.Lerp(scrolpos, new Vector2(scrolpos.x, , 3.2f * Time.deltaTime);
    }

    public void OnGUI()
    {
        GUI.skin = mySkin;
        if (showConsole)
        {
            windowRect = GUI.Window(0, windowRect, DrawMyWindow, "Console", "Window");
        }
    }

    public void DrawMyWindow(int windowID)
    {
        GUI.FocusControl("consoleField");
        GUI.SetNextControlName("consoleField");
        scrolpos = GUILayout.BeginScrollView(scrolpos);
        foreach (string m in messages)
        {
            GUILayout.Label("<color=yellow>" + m + "</color>");
        }
        GUILayout.EndScrollView();
        message = GUILayout.TextField(message);
        if (Event.current.type == EventType.KeyDown && Event.current.character == '\n' && message != "")
        {
            Write("<color=cyan>" + message + "</color>");
            if (message.StartsWith("/"))
            {
                CheckForCode(message);
            }
            message = "";
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void CheckForCode(string c)
    {
        string[] code = c.Split(' ');
        switch (code[0])
        {
            case "/connect":
                MultiplayerManager.Instance.ConnectToServer(code[1], 7777, code[3]);
                break;
            case "/create":
                MultiplayerManager.Instance.StartServer(code[1], code[2]);
                break;
            case "/close":
                showConsole = false;
                break;
            case "/disconnect":
                MultiplayerManager.Instance.DisconnectFromServer();
                break;
            case "/setPlayerName":
                MultiplayerManager.Instance.SetPlayerName(code[1]);
                break;
            case "/showPlayerList":
                MultiplayerManager.Instance.Console_ShowPlayerList();
                break;
            case "/showServerList":
                MultiplayerManager.Instance.ShowServerList();
                break;
            case "/quit":
                Application.Quit();
                break;
            case "/help":
                Write(
                    "/connect <color=white>[Server IP] [Server Port] [Server Password]</color> - Direct connection to server" + "\n" +
                    "/create <color=white>[Server Nmae] [Server Password]</color> - Create a server" + "\n" +
                    "/close - Close the console" + "\n" +
                    "/disconnect - Disconnect from current server" + "\n" +
                    "/setPlayerName <color=white>[Value]</color> - Set or change player name" + "\n" +
                    "/showPlayerList - Show player list on current connected server" + "\n" +
                    "/showServerList - Call a server list Window" + "\n" +
                    "/quit - exit game without acception"
                    );
                break;


            default:
                Write("Type [<color=green>/help</color>] to show all allow codes.");
                    break;
        }

    }
}
