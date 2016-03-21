using UnityEngine;
using System.Collections;

public class GameZoneScreenAspectAdapter : MonoBehaviour {

    void Awake() {

        Camera camera = Camera.main;
        float screenHeight = 2f * camera.orthographicSize;
        float screenwidth = screenHeight * camera.aspect;

        //Debug.Log(screenHeight);
        //Debug.Log(screenwidth);
        
        float newWidth = screenwidth / 16 * gameObject.transform.localScale.x;
        float scale = newWidth / gameObject.transform.localScale.x;
        float newHeight = gameObject.transform.localScale.y * scale;

        //Debug.Log(gameObject.transform.localScale.x);
        //Debug.Log(newWidth);
        //Debug.Log(scale);

        gameObject.transform.localScale = new Vector3(newWidth, newHeight);

    }


	// Use this for initialization
	void Start () {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer.bounds.extents.x);

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public float GetBoundX() {
        return GetComponent<SpriteRenderer>().bounds.extents.x;
    }


}
