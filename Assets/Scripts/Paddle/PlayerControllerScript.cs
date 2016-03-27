using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 0, sendInterval = 0.02f)]
public class PlayerControllerScript : NetworkBehaviour {

    public GameObject ball;
    private GameObject initialBall;

	public GameObject playerCamera;
	private GameObject initialPlayerCamera;

    public float maxSpeed;
    private float sidePanelWidth;

    private float width;
    private float height;

    Vector2 min;
    Vector2 max;

    void Awake()
    {


        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

        GameObject gameZone = GameObject.Find("GameZone");

        Sprite gameZoneSprite = gameZone.GetComponent<SpriteRenderer>().sprite;
        float gameZoneWidth = gameZoneSprite.bounds.size.x * gameZone.transform.localScale.x;
        float gameZoneheight = gameZoneSprite.bounds.size.y * gameZone.transform.localScale.y;

        min = new Vector2(-gameZoneWidth/2, -gameZoneheight/2);
        max = new Vector2(gameZoneWidth/2, gameZoneheight/2);

        max.x = max.x - width / 2;
        min.x = min.x + width / 2;
        max.y = max.y - height / 2;
        min.y = min.y + height / 2;

        //GetComponent<NetworkTransformVisualizer>().visualizerPrefab = gameObject;
    }

    // Use this for initialization
    void Start () {


        sidePanelWidth = GameObject.Find("WallLeft").GetComponent<Renderer>().bounds.size.x;

        //Debug.Log(sidePanelWidth);



		if (isLocalPlayer) {
			createPlayerCamera ();
		}

        //CmdInitializeBall();
    }

    void Update()
    {

        if (isLocalPlayer)
        {
            Move();
            //CmdDoInitialFire();
            Fire();
        }

        //CmdHoldBall();


    }

    // Update is called once per frame
    //void FixedUpdate () {

    //    if (isLocalPlayer) {
    //        Move();
    //        //CmdDoInitialFire();
    //        Fire();
    //    }

    //    //CmdHoldBall();


    //}

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

        possition = Vector2.Lerp(possition, possition + direction * maxSpeed, Time.deltaTime);

        //possition += direction * maxSpeed * Time.deltaTime;

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
        //initialBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -4);
        //initialBall.GetComponent<BallScript>().parentNetId = GetComponent<NetworkIdentity>().netId;

        NetworkServer.Spawn(initialBall);
        //NetworkServer.SpawnWithClientAuthority(initialBall, base.connectionToClient);
        initialBall = null;

    }

}
