using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

    private bool MouseDown=false;
    private bool Dead = false;
    private bool PlatformCheck = false;
    private bool WallCheck = false;

    private int Inc = 1;
    private float Pos;
	private int WallTouch;
	private int PlatformTouch;
    [HideInInspector]
    public int Score;

    public GameObject platform;
    public int AmountIncDefault = 1;
    public int AmountIncWall = 2;
    public int AmountIncPlatform = 3;

    // Use this for initialization
    void Start () {
        Pos = 1.0f;

}
	
	// Update is called once per frame
	void Update () {

        if(!MouseDown)
        {
            gameObject.GetComponent<Transform>().position =new Vector3(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y, platform.GetComponent<Transform>().position.z+Pos);
        }

        if (Input.GetKey(KeyCode.Mouse0) && !MouseDown)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,25);
            MouseDown = true;
		}
			

	}

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.transform.parent.gameObject.tag == "EnemyPlatform" || col.gameObject.tag == "EnemyPlatform")
        {
			if (WallCheck == true) {
				Inc += AmountIncWall;
			} else if (PlatformCheck == true) {
				Inc += AmountIncPlatform;
			} else if (PlatformCheck == false)
				Inc = AmountIncPlatform;
            Destroy(col.gameObject.transform.parent.gameObject);
            PlatformCheck = true;
            WallCheck = false;
        }
        else 
            PlatformCheck = false;

		if (col.gameObject.tag == "Platform" || col.gameObject.transform.parent.gameObject.tag == "Platform") {
			Inc = AmountIncDefault;
		}

        if (col.gameObject.tag == "Wall")
        {	
			if (WallCheck == false)
				Inc = AmountIncWall;
            WallCheck = true;
            PlatformCheck = false;
        }
        else WallCheck = false;

		if (col.gameObject.tag=="DeadZone")
		{
			Destroy(gameObject);
			Dead = true;
		}

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "TrigCol")
        {
			
			//Debug.Log("Inc "+Inc);
            Score += Inc;

        } 

        if (col.gameObject.tag == "DeadZone")
        {
            Destroy(gameObject);
            Dead = true;
        }
    }
}
