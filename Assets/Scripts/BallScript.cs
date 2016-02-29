using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float initialSpeed;
    public float maxSpeed;
    public float acceleration;

    public Vector2 velocity;

    private float horizontalMove;
    private Rigidbody2D rigidbody2D;
    private Vector2 newDirection;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(0, -initialSpeed);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {

        velocity = rigidbody2D.velocity;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerTag") {

            horizontalMove = Input.GetAxisRaw("Horizontal");
            float magnitude = rigidbody2D.velocity.magnitude;

            if (horizontalMove == 0)
                newDirection = rigidbody2D.velocity;

            if (horizontalMove > 0)
                newDirection = rigidbody2D.velocity + new Vector2(1, 0);

            if (horizontalMove < 0)
                newDirection = rigidbody2D.velocity + new Vector2(-1, 0);

            newDirection = Vector2.ClampMagnitude(newDirection * 100, magnitude);

            rigidbody2D.velocity = Vector2.ClampMagnitude(newDirection * (1 + acceleration), maxSpeed);

        }

    }

}
