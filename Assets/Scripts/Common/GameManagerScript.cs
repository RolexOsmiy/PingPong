using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public GameObject buttonPlay;

    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
    }

    GameManagerState GMState;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void UpdateGameManagerState()
    {

        switch (GMState)
        {
            case GameManagerState.Opening:

                break;
            case GameManagerState.GamePlay:

                SceneManager.LoadScene("Level");

                break;
            case GameManagerState.GameOver:
                break;

        }

    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
        Debug.Log("change game status");
    }

    public void ChangeGameManagerStateToGamePlay()
    {
        SetGameManagerState(GameManagerState.GamePlay);
    }

    public void ChangeGameManagerStateToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

}
