using UnityEngine;
using System.Collections;

public class MoveHorizontal : MonoBehaviour {

    public float maxSpeed;
    float halfWidth;
    float halfHeight;

    // Use this for initialization
    void Start () {

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        halfWidth = sprite.bounds.size.x/2;
        halfHeight = sprite.bounds.size.y/2;

    }
	
	// Update is called once per frame
	void Update () {
        Move();

    }

    void Move() {
        Vector2 possition = transform.position;

        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, 0).normalized;

        //Debug.DrawLine(possition, direction);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - halfWidth;
        min.x = min.x + halfWidth;
        max.y = max.y - halfHeight;
        min.y = min.y + halfHeight;

        possition += direction * maxSpeed * Time.deltaTime;

        possition.x = Mathf.Clamp(possition.x, min.x, max.x);
        possition.y = Mathf.Clamp(possition.y, min.y, max.y);

        transform.position = possition;
    }
}
