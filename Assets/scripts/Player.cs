using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Vector3 mousePos;
    public float minZ;
    public float maxZ;

    // Use this for initialization
    void Start () {
	    
	}

	// Update is called once per frame
	void FixedUpdate () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            mousePos = hit.point;

        gameObject.GetComponent<Transform>().position = new Vector3(Mathf.Clamp(mousePos.y,minZ,maxZ), gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.x);
	}
    

	
}
