﻿using UnityEngine;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {

    public GameObject ball;

    float width;
    float height;
    GameObject initialBall;
    public float maxSpeed;
    public float sidePanelWidth;

    // Use this for initialization
    void Start () {

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

        InitializeBall();

    }
	
	// Update is called once per frame
	void Update () {

        Move();

        if (initialBall != null) {

            initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);

            if (Input.GetKey("space"))
            {
                initialBall.GetComponent<BallScript>().MoveUp();
                initialBall = null;
            }


        }





    }

    void InitializeBall() {

        initialBall = (GameObject) Instantiate(ball);
        initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);


    }

    void Move()
    {
        Vector2 possition = transform.position;

        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, 0).normalized;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - width / 2 - sidePanelWidth;
        min.x = min.x + width / 2 + sidePanelWidth;
        max.y = max.y - height / 2;
        min.y = min.y + height / 2;

        possition += direction * maxSpeed * Time.deltaTime;

        possition.x = Mathf.Clamp(possition.x, min.x, max.x);
        possition.y = Mathf.Clamp(possition.y, min.y, max.y);

        transform.position = possition;
    }


}