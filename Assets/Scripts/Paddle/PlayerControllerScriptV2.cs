using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerControllerScriptV2 : NetworkBehaviour
{

    public GameObject ball;
    private GameObject initialBall;

    public float maxSpeed;
    public float sidePanelWidth;

    private float width;
    private float height;

    private Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();

        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

        CmdInitializeBall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isLocalPlayer)
            return;

        Move();
        HoldBall();


    }

    [Command]
    void CmdInitializeBall()
    {
        initialBall = (GameObject)Instantiate(ball);
        initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);
        NetworkServer.Spawn(initialBall);
    }

    void Move()
    {
        Vector2 possition = transform.position;

        float moveVector = Input.GetAxisRaw("Horizontal");

        rigidbody2D.velocity = new Vector2(moveVector * 10, rigidbody2D.velocity.y);


    }


    void HoldBall()
    {
        if (initialBall != null)
        {

            initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);

            if (Input.GetKey("space"))
            {
                initialBall.GetComponent<BallScript>().MoveUp();
                initialBall = null;
            }

        }
    }
}
