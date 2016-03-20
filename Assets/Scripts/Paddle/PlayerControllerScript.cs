using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerControllerScript : NetworkBehaviour {

    public GameObject ball;
    private GameObject initialBall;

	public GameObject playerCamera;
	private GameObject initialPlayerCamera;

    public float maxSpeed;
    private float sidePanelWidth;

    private float width;
    private float height;

    void Awake()
    {
        GetComponent<NetworkTransform>().sendInterval = 0.001f;

        //GetComponent<NetworkTransformVisualizer>().visualizerPrefab = gameObject;
    }

    // Use this for initialization
    void Start () {

        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sidePanelWidth = GameObject.Find("WallLeft").GetComponent<Renderer>().bounds.size.x;

        Debug.Log(sidePanelWidth);

        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

		if (isLocalPlayer) {
			createPlayerCamera ();
		}

        //CmdInitializeBall();
    }

    // Update is called once per frame
    void Update () {

        Debug.Log(GetComponent<NetworkTransform>().sendInterval);

        if (isLocalPlayer) {
            Move();
            //CmdDoInitialFire();
            Fire();
        }


        //CmdHoldBall();


    }

    [Command]
    void CmdInitializeBall()
    {
        initialBall = (GameObject)Instantiate(ball);
        initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);
        initialBall.transform.parent = gameObject.transform;
        NetworkServer.Spawn(initialBall);        
    }
		
	void createPlayerCamera(){
		initialPlayerCamera = (GameObject)Instantiate(playerCamera);
		if (gameObject.transform.position.y > 0) {
			initialPlayerCamera.transform.position = new Vector3 (0, 0, 10);
			initialPlayerCamera.transform.Rotate (0, 180, 180);
		} else {
			initialPlayerCamera.transform.position = new Vector3 (0, 0, -10);
			initialPlayerCamera.transform.Rotate (0, 0, 0);
		}
		NetworkServer.Spawn(initialPlayerCamera);
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

    [Command]
    void CmdDoInitialFire() {
        if (initialBall != null)
        {
            initialBall.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height);

            if (Input.GetKey("space"))
            {
                initialBall.transform.parent = null;
                initialBall.GetComponent<BallScript>().MoveUp();
                initialBall = null;
            }
        }
    }

    void Fire() {
        if (Input.GetKeyDown("space"))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire() {

        float intialBallDeviation = height;
        if (gameObject.transform.position.y > 0)
        {
            intialBallDeviation = -intialBallDeviation;
        }

        initialBall = (GameObject)Instantiate(ball, new Vector2(transform.position.x, transform.position.y + intialBallDeviation), transform.rotation);
            //initialBall.GetComponent<BallScript>().MoveUp();
            initialBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -4);
            NetworkServer.Spawn(initialBall);
            initialBall = null;

    }

}
