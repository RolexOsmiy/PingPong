using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 0, sendInterval = 0.01f)]
public class PaddleSyncPosition : NetworkBehaviour {

    public float transformLerpRate = 30f;
    public float rotationLerpRate = 15;
    private Transform paddleTransform;
    [SyncVar]
    private Vector2 syncPosition;
    [SyncVar]
    private Quaternion syncObjectRotation;
    

    void Awake() {
        paddleTransform = gameObject.transform;
        syncPosition = paddleTransform.position;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        TranmitPosition();
        LerpPosition();

        LerpRotation();
        TransmitRotation();

    }

    // Sync Transform

    void LerpPosition() {
        if (!isLocalPlayer) {
            paddleTransform.position = Vector2.Lerp(paddleTransform.position, syncPosition, Time.deltaTime * transformLerpRate);
            //paddleTransform.position = syncPosition;
        }
    }

    [Command]
    void CmdProvidePosotionToServer(Vector2 position) {
        syncPosition = position;
    }

    [ClientCallback]
    void TranmitPosition() {
        if (isLocalPlayer) {
            CmdProvidePosotionToServer(paddleTransform.position);
        }
    }

    // Sync Rotation

    private void LerpRotation()
    {

        if (!isLocalPlayer)
        {
            paddleTransform.rotation = Quaternion.Lerp(paddleTransform.rotation, syncObjectRotation, Time.deltaTime * rotationLerpRate);
        }

    }

    [Command]
    private void Cmd_ProvideRotationToServer(Quaternion rotation)
    {
        syncObjectRotation = rotation;
    }

    [ClientCallback]
    private void TransmitRotation()
    { // Send rotation to server
        if (isLocalPlayer)
        {
            Cmd_ProvideRotationToServer(paddleTransform.rotation);
        }
    }





}
