using UnityEngine;
using System.Collections;

public class MenuControllerScript : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject singlePlayerMenu;

    public enum MenuSection
    {
        Main,
        SinglePlayer,
        CampaingMenu
    }

    MenuSection manuSection;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HideAllMenuSections() {

        GameObject[] menuSections = GameObject.FindGameObjectsWithTag("MenuTag");
        foreach (GameObject menuSection in menuSections)
        {
            menuSection.SetActive(false);
        }

    }

    public void ShowSinglePlayerMenu()
    {
        singlePlayerMenu.SetActive(true);
    }

}
