using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfoLoader : MonoBehaviour {

	public int campaineId;
	public int levelId;

    private Dictionary<string, Dictionary<string, PlayerLevelInfo>> campaigns;

    // Use this for initialization
    void Start () {

        GenerateLevelsInfo();
        SetLevelsInfo();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // tmp method, toDelete
    void GenerateLevelsInfo() {

        campaigns = new Dictionary<string, Dictionary<string, PlayerLevelInfo>>();

        for (int i = 0; i < 5; i++)
        {

            Dictionary<string, PlayerLevelInfo> levels = new Dictionary<string, PlayerLevelInfo>();

            for (int j = 0; j < 5; j++)
            {

                PlayerLevelInfo playerLevelInfo = new PlayerLevelInfo();
                playerLevelInfo.status = Random.Range(0, 100) < 50;

                if (!playerLevelInfo.status)
                {
                    playerLevelInfo.stars = 0;
                    playerLevelInfo.score = 0;
                }
                else {
                    playerLevelInfo.stars = Random.Range(1,3);
                    playerLevelInfo.score = Random.Range(1000, 5000);
                }

                levels.Add("L" + (j + 1), playerLevelInfo);

            }

            campaigns.Add("C" + (i + 1), levels);

        }

        foreach (KeyValuePair<string, Dictionary<string, PlayerLevelInfo>> campaignsEntry in campaigns)
        {

            foreach (KeyValuePair<string, PlayerLevelInfo> levelsEntry in campaignsEntry.Value)
            {
                Debug.Log("=====================================");
                Debug.Log("status = " + levelsEntry.Value.status);
                Debug.Log("stars = " + levelsEntry.Value.stars);
                Debug.Log("score = " + levelsEntry.Value.score);
                Debug.Log("=====================================");
            }

        }

    }

    void SetLevelsInfo() {

        PlayerLevelInfo playerLevelInfo = campaigns["C" + campaineId]["L" + levelId];

        bool status = playerLevelInfo.status;
        int stars = playerLevelInfo.stars;
        int score = playerLevelInfo.score;

        gameObject.transform.Find("MissionResultScore").GetComponent<UnityEngine.UI.Text>().text = score.ToString();

        string starsText = "";
        for (int i = 0; i < stars; i++) {
            starsText += "*";
        }

        gameObject.transform.Find("MissionResultStars").GetComponent<UnityEngine.UI.Text>().text = starsText;

    }


    public class PlayerLevelInfo {

        public bool status;
        public int stars;
        public int score;

        public PlayerLevelInfo() {}


    }



}
