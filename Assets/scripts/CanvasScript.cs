using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	public Text textScore;
	public Text textExpirience;
	public GameObject Sphere;

	// Use this for initialization
	void Start () {
		Sphere = GameObject.FindGameObjectWithTag ("Ball");
	}
	
	// Update is called once per frame
	void Update () {
		if (Sphere != null)
			textExpirience.enabled = false;
		else textExpirience.enabled = true;
		textScore.text = "Your Score: "+Sphere.GetComponent<SphereScript>().Score;
		textExpirience.text = "Expirience +"+(Sphere.GetComponent<SphereScript>().Score)*5;
	}
}
