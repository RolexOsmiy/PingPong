using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawnerScript : MonoBehaviour {

    public GameObject paddle;
    private GameObject initialPaddle;

    private List <Vector2> spawnPositions = new List<Vector2>();

    // Use this for initialization
    void Start () {

        spawnPositions.Add(new Vector2(0, -4.85f));
        spawnPositions.Add(new Vector2(0, 4.85f));

        InitializePaddle();


    }
	
	// Update is called once per frame
	void Update () {

    }

    void InitializePaddle()
    {

        initialPaddle = (GameObject)Instantiate(paddle);

        Vector2 position = spawnPositions[0];
        spawnPositions.RemoveAt(0);
        initialPaddle.transform.position = position;
    }




}
