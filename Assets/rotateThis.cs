using UnityEngine;
using System.Collections;

public class rotateThis : MonoBehaviour {
	public float rotatePerSecondx = 0f;
	public float rotatePerSecondy = 0.5f;
	public float rotatePerSecondz = 0f;

	// Update is called once per frame
	void Update () {
		transform.Rotate (rotatePerSecondx * Time.deltaTime,rotatePerSecondy * Time.deltaTime,rotatePerSecondz * Time.deltaTime);
	}
}
