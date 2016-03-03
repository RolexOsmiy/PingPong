using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float initialSpeed;
    public float maxSpeed;
    public float acceleration;
    public float randomDeviation;
    public float magnitude;
    public Vector2 velocity;

    private float horizontalMove;
    private Rigidbody2D rigidbody2D;
    private Vector2 newDirection;

    // Use this for initialization
    void Start () {

        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(0, initialSpeed);
    }
	
	// Update is called once per frame
	void Update () {


    }

    void FixedUpdate() {

        magnitude = rigidbody2D.velocity.magnitude;
        velocity = rigidbody2D.velocity;

        Debug.DrawLine(gameObject.transform.position, (Vector2)gameObject.transform.position + rigidbody2D.velocity);


    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        magnitude = rigidbody2D.velocity.magnitude;
        velocity = rigidbody2D.velocity;

        Debug.Log(velocity);

        if (coll.gameObject.tag == "PlayerTag") {

            horizontalMove = Input.GetAxisRaw("Horizontal");
            

            if (horizontalMove == 0)
                newDirection = rigidbody2D.velocity;

            if (horizontalMove > 0)
                newDirection = rigidbody2D.velocity + Vector2.right;

            if (horizontalMove < 0)
                newDirection = rigidbody2D.velocity + Vector2.left;

            //rigidbody2D.velocity = Vector2.ClampMagnitude(newDirection * (1 + acceleration), maxSpeed);

            newDirection = newDirection * (initialSpeed / newDirection.magnitude);
            rigidbody2D.velocity = newDirection;

        }

        if (coll.gameObject.tag == "WallTag")
        {

            //newDirection = new Vector2(rigidbody2D.velocity.x + Random.Range(-randomDeviation, randomDeviation), rigidbody2D.velocity.y + Random.Range(-randomDeviation, randomDeviation));

            Debug.Log("Wall Collision");

            //newDirection = newDirection * (initialSpeed / newDirection.magnitude);

            //newDirection = newDirection * (initialSpeed / newDirection.magnitude);
            //rigidbody2D.velocity = newDirection;

            //newDirection = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + 0.2f);

            //Debug.Log(newDirection);
            //Debug.Log(newDirection.magnitude);
            //newDirection = newDirection * (initialSpeed / newDirection.magnitude);

            //Debug.Log(newDirection);
            //Debug.Log(newDirection.magnitude);

            //rigidbody2D.velocity = newDirection;

        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "WallTag")
        {
            Debug.Log("Wall Collision");
            rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x, rigidbody2D.velocity.y);
        }

        if (coll.gameObject.tag == "RoofTag")
        {
            Debug.Log("Wall Collision");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);
        }

    }

}
