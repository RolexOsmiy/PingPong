using UnityEngine;
using System.Collections;

public class backgroundImageRandom : MonoBehaviour {

	public Sprite[] mSpriteImage = new Sprite[20];
	public int maxSpriteImage = 20;
	private int numberImage;
	// Use this for initialization
	void Start () {
		numberImage = Random.Range (0,maxSpriteImage);
		gameObject.GetComponent<SpriteRenderer>().sprite = mSpriteImage[numberImage];
	}

}
