using UnityEngine;
using System.Collections;

public class PlayerControllerScriptNew : MonoBehaviour
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


}
