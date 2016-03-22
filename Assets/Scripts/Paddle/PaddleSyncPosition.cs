using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 0, sendInterval = 0.02f)]
public class PaddleSyncPosition : NetworkBehaviour {

    [SyncVar]
    private Vector2 syncPosition;
    [SerializeField]
    Transform paddleTransform;
    [SerializeField]
    float lerpRate = 15;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        TranmitPosition();
        LerpPosition();
    }

    void LerpPosition() {
        if (!isLocalPlayer) {
            //paddleTransform.position = Vector2.Lerp(paddleTransform.position, syncPosition, Time.deltaTime * lerpRate);
            paddleTransform.position = syncPosition;
        }
    }

    [Command]
    void CmdProvidePosotionToServer(Vector2 pos) {
        syncPosition = pos;
    }

    [ClientCallback]
    void TranmitPosition() {
        if (isLocalPlayer) {
            CmdProvidePosotionToServer(paddleTransform.position);
        }
    }





}
