using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public enum GameManagerState
    {
        Opening,
        SinglePlayer,
        MultiPlayer,
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
            case GameManagerState.SinglePlayer:

                SceneManager.LoadScene("Level");
                break;
            case GameManagerState.MultiPlayer:

                SceneManager.LoadScene("Multi");
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



    public void ChangeGameManagerStateToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public void ChangeGameManagerStateToSinglePlayer()
    {
        SetGameManagerState(GameManagerState.SinglePlayer);
    }

    public void ChangeGameManagerStateToMultiPlayer()
    {
        SetGameManagerState(GameManagerState.MultiPlayer);
    }

}
