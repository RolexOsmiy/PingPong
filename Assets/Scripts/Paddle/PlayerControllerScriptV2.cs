using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerControllerScriptV2 : NetworkBehaviour
{

    public GameObject ball;
    private GameObject initialBall;

    public float maxSpeed;
    private float sidePanelWidth;

    private float width;
    private float height;

    private Rigidbody2D rigidbody2D;

    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // Use this for initialization
    void Start()
    {
        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sidePanelWidth = GameObject.Find("WallLeft").GetComponent<Renderer>().bounds.size.x;

        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

        max.x = max.x - width / 2 - sidePanelWidth;
        min.x = min.x + width / 2 + sidePanelWidth;

        rigidbody2D = GetComponent<Rigidbody2D>();

        CmdInitializeBall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isLocalPlayer)
            return;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

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

        if (moveVector > 0 && transform.position.x >= max.x) {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }

        if (moveVector < 0 && transform.position.x <= min.x)
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }




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
