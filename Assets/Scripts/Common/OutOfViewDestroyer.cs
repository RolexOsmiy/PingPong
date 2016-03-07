using UnityEngine;
using System.Collections;

public class OutOfViewDestroyer : MonoBehaviour {

    private Vector2 cameraMin;
    private Vector2 cameraMax;

    private float width;
    private float height;

    // Use this for initialization
    void Start () {

        cameraMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cameraMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        cameraMin = new Vector2(cameraMin.x - width/2, cameraMin.y - height/2);
        cameraMax = new Vector2(cameraMax.x + width/2, cameraMax.y + height/2);

    }
	
	// Update is called once per frame
	void Update () {

        DestroyIfOutOfView();

    }

    public void DestroyIfOutOfView()
    {

        if ((transform.position.x < cameraMin.x) || (transform.position.x > cameraMax.x) ||
        (transform.position.y < cameraMin.y) || (transform.position.y > cameraMax.y))
        {
            Destroy(gameObject);
        }

    }


}
