﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Server : MonoBehaviour {
	
	public GameObject playerPrefab;	// Персонаж игрока
    public GameObject ballPrefab;
    public string ip = "127.0.0.1";	// ip для создания или подключения к серверу
	public string port = "5300";	// Порт
	public bool connected;			// Статус подключения
	private GameObject player;			// Объект для ссылки на игрока
	public bool _visible = false;	// Статус показа меню

    private List<Vector2> spawnPositions = new List<Vector2>();
    Vector2 spawnPosition;

    void Start(){
        spawnPositions.Add(new Vector2(0, 4.85f));
        spawnPositions.Add(new Vector2(0, -4.85f));
    }
	
	// На каждый кадр
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape)) 
			_visible = !_visible;
	}
	
	// На каждый кадр для прорисовки кнопок
	void OnGUI () {
		// Если мы на сервере
		if(connected) {
			if(_visible) {
				GUI.Label(new Rect((Screen.width - 120)/2, Screen.height/2 - 35, 120, 30), "Присоединились: " + Network.connections.Length);
				if(GUI.Button(new Rect((Screen.width - 100)/2, Screen.height/2, 100, 30), "Отключиться")) 
					Network.Disconnect(200);
				
				if(GUI.Button(new Rect((Screen.width - 100)/2, Screen.height/2 + 35, 100, 30), "Выход"))
					Application.Quit();
			}
		//Если мы в главном меню
		} else {
			GUI.Label(new Rect((Screen.width - 100)/2, Screen.height/2-60, 100, 20), "Ip");
			GUI.Label(new Rect((Screen.width - 100)/2, Screen.height/2-30, 100, 20), "Порт");
			ip = GUI.TextField(new Rect((Screen.width - 100)/2+35, Screen.height/2-60, 100, 20), ip);
			port = GUI.TextField(new Rect((Screen.width - 100)/2+35, Screen.height/2-30, 50, 20), port);
			
			if(GUI.Button(new Rect((Screen.width - 110)/2, Screen.height/2, 110, 30), "Присоединиться"))
				Network.Connect(ip, Convert.ToInt32(port));
			
			if(GUI.Button(new Rect((Screen.width - 110)/2, Screen.height/2 + 35, 110, 30), "Создать сервер"))
				Network.InitializeServer(10, Convert.ToInt32(port), false);
			
			if(GUI.Button(new Rect((Screen.width - 110)/2, Screen.height/2 + 70, 110, 30), "Выход"))
				Application.Quit();
		}
	}
	
	// Вызывается когда мы подключились к серверу
	void OnConnectedToServer () {
        Debug.Log("OnConnectedToServer");
        spawnPosition = spawnPositions[1];
        CreatePlayer();
	}
	
	// Когда мы создали сервер
	void OnServerInitialized () {
        Debug.Log("OnServerInitialized");
        spawnPosition = spawnPositions[0];
        CreatePlayer();
    }
	
	// Создание игрока
	void CreatePlayer () {
        Debug.Log("CreatePlayer");
        connected = true;
        //GetComponent<Camera>().enabled = false;
        //GetComponent<Camera>().gameObject.GetComponent<AudioListener>().enabled = false;

        player = (GameObject)Network.Instantiate(playerPrefab, spawnPosition, transform.rotation, 1);
        //player.GetComponent<PlayerControllerMultiScript>().InitializeBall();

        //CreateInitialBall();

        //_go.transform.LookAt(Vector2.zero);
        //Debug.Log (_go.transform.rotation);
        //float temp = _go.transform.rotation.x; 
        //_go.transform.rotation = (Quaternion.Euler(0,0, temp + 90));

        //Debug.Log("Rotarion" + _go.transform.rotation);

        //_go.transform.GetComponentInChildren<Camera>().GetComponent<Camera>().enabled = true;
        //_go.transform.GetComponentInChildren<AudioListener>().enabled = true;
    }

    void CreateInitialBall() {
        float height = player.GetComponent<PlayerControllerMultiScript>().height;

        float intialBallDeviation = height;
        if (player.transform.position.y > 0)
        {
            intialBallDeviation = -intialBallDeviation;
        }

        Vector2 ballspawnPosition = new Vector2(player.transform.position.x, player.transform.position.y + intialBallDeviation);
        player = (GameObject)Network.Instantiate(ballPrefab, ballspawnPosition, transform.rotation, 1);
        //player.transform.parent = gameObject.transform;
    }

	// При отключении от сервера
	void OnDisconnectedFromServer (NetworkDisconnection info) {
		connected = false;
		//GetComponent<Camera>().enabled = true;
		//GetComponent<Camera>().gameObject.GetComponent<AudioListener>().enabled = true;
		//Application.LoadLevel(Application.loadedLevel);
	}
	
	// Вызывается каждый раз когда игрок отсоеденяется от сервера
	void OnPlayerDisconnected (NetworkPlayer pl) {
		Network.RemoveRPCs(pl);
		Network.DestroyPlayerObjects(pl);
	}

}
