using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        magnitude = rigidbody2D.velocity.magnitude;
        velocity = rigidbody2D.velocity;

        Debug.DrawLine(gameObject.transform.position, (Vector2)gameObject.transform.position + rigidbody2D.velocity, Color.red);

        if (magnitude != 0 && magnitude < minSpeed) {

            rigidbody2D.velocity = rigidbody2D.velocity * (minSpeed / rigidbody2D.velocity.magnitude);

        }




    }

    void FixedUpdate() {


    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        magnitude = rigidbody2D.velocity.magnitude;
        velocity = rigidbody2D.velocity;

        if (coll.gameObject.tag == "PlayerPaddleTag") {

            horizontalMove = Input.GetAxisRaw("Horizontal");
            

            if (horizontalMove == 0)
                newDirection = rigidbody2D.velocity;

            if (horizontalMove > 0) {
                newDirection = rigidbody2D.velocity + Vector2.right;
                newDirection += new Vector2(Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation), Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation));
            }


            if (horizontalMove < 0) {
                newDirection = rigidbody2D.velocity + Vector2.left;
                newDirection += new Vector2(Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation), Random.Range(-playerTouchRandomhDeviation, playerTouchRandomhDeviation));
            }


            //rigidbody2D.velocity = Vector2.ClampMagnitude(newDirection * (1 + acceleration), maxSpeed);

            newDirection = newDirection * (magnitude / newDirection.magnitude);
            rigidbody2D.velocity = newDirection;

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

        if (coll.gameObject.tag == "BlockHorizontalSurfaceTag")
        {
            Debug.Log("BlockHorizontalSurfaceTag Collision");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);
            Destroy(coll.gameObject.transform.parent.gameObject);

        }

        if (coll.gameObject.tag == "BlockVerticalSurfaceTag")
        {
            Debug.Log("BlockVerticalSurfaceTag Collision");
            rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x, rigidbody2D.velocity.y);
            Destroy(coll.gameObject.transform.parent.gameObject);
        }

    }

    public void MoveUp() {

        rigidbody2D.velocity = new Vector2(0, initialSpeed);
    }

}
