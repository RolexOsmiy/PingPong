using UnityEngine;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {

    public GameObject ball;

    float halfWidth;
    float halfHeight;
    GameObject initialBall;

    // Use this for initialization
    void Start () {

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        halfWidth = sprite.bounds.size.x / 2;
        halfHeight = sprite.bounds.size.y / 2;

        //initialBall = (GameObject)Instantiate(ball);
        //initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + halfHeight);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
