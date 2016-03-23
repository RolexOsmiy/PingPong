using UnityEngine;
using System.Collections;

public class GUIPanels : MonoBehaviour 
{
    public GUIStyle style;
    public GUIStyle buttonStyle;

    public int buttonHight = 70;


    public GameObject[] Walls;

    public void FixedUpdate()
    {

    }

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, (Screen.width / 16) * 2, Screen.height), "", style);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight)))
        {
            Debug.Log("Heeey!");
        }
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.FlexibleSpace();
        GUILayout.Button("<-", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - ((Screen.width / 16) * 2), 0, (Screen.width / 16) * 2, Screen.height), "", style);
        GUILayout.FlexibleSpace();
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.Button("Skill", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.FlexibleSpace();
        GUILayout.Button("->", buttonStyle, GUILayout.Height(buttonHight));
        GUILayout.EndArea();
    }
}
