using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public GameObject MMPlane;
    public GameObject ButBack;

	public void Exit () 
    {
        Application.Quit();
	}
	
	public void Inventory () 
    {
        MMPlane.SetActive(true);
        ButBack.SetActive(true);
    }

    public void Play ()
    {
        Application.LoadLevel(2);
    }

    public void Options()
    {
        MMPlane.SetActive(true);
        ButBack.SetActive(true);
    }

    public void Back()
    {
        MMPlane.SetActive(false);
        ButBack.SetActive(false);
    }
}