using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

//[NetworkSettings(channel = 0, sendInterval = 0.02f)]
public class BallScript : NetworkBehaviour {

    public float initialSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float acceleration;
    public float playerTouchRandomhDeviation;
    public float magnitude;
    public Vector2 velocity;

    private float horizontalMove;
    private Rigidbody2D rigidbody2D;
    private Vector2 newDirection;

    // used for velocity calculation
    private Vector2 lastPosition;
    //private int initialBlocks;
    //private int countBlocks;

    //[SyncVar]
    //public NetworkInstanceId parentNetId;

    void Awake()
    {
        //GetComponent<NetworkTransform>().sendInterval = 0.05f;
        //Debug.Log(GetComponent<NetworkTransform>().sendInterval);
        
    }

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
            Destroy(rigidBody2D);
            GetComponent<NetworkTransform>().enabled = false;
            GetComponent<BallSyncPosition>().enabled = true;
        }
        else
        {
            rigidbody2D = GetComponent<Rigidbody2D>();


            float tmpSpd = 4;

            if (gameObject.transform.position.y < 0)
            {
                tmpSpd = -tmpSpd;
            }

            rigidbody2D.velocity = new Vector2(0, -tmpSpd);
        }
        //GameObject[] blocks = GameObject.FindGameObjectsWithTag("EnemyBlockTag");
        //initialBlocks = blocks.Length;
        //countBlocks = initialBlocks; 

    }
	
	// Update is called once per frame
	void Update () {

        //GameObject[] blocks = GameObject.FindGameObjectsWithTag("EnemyBlockTag");
        //countBlocks = blocks.Length;

        //Debug.Log("blocks minus = " + (initialBlocks - countBlocks));
    }

    void FixedUpdate()
    {

        magnitude = rigidbody2D.velocity.magnitude;

        if (magnitude != 0 && magnitude < minSpeed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity * (minSpeed / magnitude);
        }

        Debug.DrawLine(gameObject.transform.position, (Vector2)gameObject.transform.position + rigidbody2D.velocity, Color.red);

        // Get pos 2d of the ball.
        Vector2 position = transform.position;

        // Velocity calculation. Will be used for the bounce
        velocity = position - lastPosition;
        lastPosition = position;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        magnitude = rigidbody2D.velocity.magnitude;

        if (coll.gameObject.tag == "PlayerPaddleTag") {

            //horizontalMove = Input.GetAxisRaw("Horizontal");

            newDirection = rigidbody2D.velocity;


            //if (horizontalMove == 0)
            //    newDirection = rigidbody2D.velocity;

            //if (horizontalMove > 0) {
            //    newDirection = rigidbody2D.velocity + Vector2.right;
            //    newDirection += new Vector2(Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation), Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation));
            //}


            //if (horizontalMove < 0) {
            //    newDirection = rigidbody2D.velocity + Vector2.left;
            //    newDirection += new Vector2(Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation), Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation));
            //}


            //rigidbody2D.velocity = Vector2.ClampMagnitude(newDirection * (1 + acceleration), maxSpeed);

            Debug.Log(newDirection);
            Debug.Log(magnitude);
            Debug.Log(rigidbody2D.velocity);

            newDirection = newDirection * (magnitude / newDirection.magnitude);
            rigidbody2D.velocity = newDirection;

            Debug.Log(newDirection);

        }

        if (coll.gameObject.tag == "EnemyBlockTag")
        {
            Debug.Log("EnemyBlockTag Collision");

            // Normal
            Vector2 normal = coll.contacts[0].normal;

            //Direction
            //Vector2 normalizedVelocity = velocity.normalized;

            // Reflection
            Vector2 reflection = Vector2.Reflect(velocity, normal).normalized;

            // Assign normalized reflection with the constant speed
            rigidbody2D.velocity = reflection * magnitude;

            //Debug.DrawLine(coll.contacts[0].point, normal, Color.green, 1f);

            Destroy(coll.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "WallTag")
        {
            Debug.Log("Wall Collision");
            rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x, rigidbody2D.velocity.y);
            //rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x + Random.Range(-0.1f, 0.1f), rigidbody2D.velocity.y + Random.Range(-0.1f, 0.1f));
        }

        if (coll.gameObject.tag == "RoofTag")
        {
            Debug.Log("Roof Collision");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);
        }

        if (coll.gameObject.tag == "PlayerPaddleTag")
        {
            Debug.Log("PlayerPaddleTag Collision");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);

        }

    }

    public void MoveUp() {

        rigidbody2D.velocity = new Vector2(0, initialSpeed);
    }

    public override void OnStartClient()
    {
        //GameObject parentObject = ClientScene.FindLocalObject(parentNetId);
        //transform.SetParent(parentObject.transform);
    }


}
