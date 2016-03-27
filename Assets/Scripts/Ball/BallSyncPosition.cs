using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 0, sendInterval = 0.01f)]
public class BallSyncPosition : NetworkBehaviour
{

    public float transformLerpRate = 30f;
    public float rotationLerpRate = 15;
    private Transform ballTransform;
    [SyncVar]
    private Vector2 syncPosition = new Vector2(3,3);
    [SyncVar]
    private Quaternion syncRotation;
    [SyncVar]
    private Vector2 syncVelocity;

    private Rigidbody2D rigidbody2D;


    void Awake()
    {
        ballTransform = gameObject.transform;
        syncPosition = ballTransform.position;
    }

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("syncPosition = " + syncPosition);
        TranmitPosition();
        LerpPosition();

        //LerpRotation();
        //TransmitRotation();

    }

    // Sync Transform

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            ballTransform.position = Vector2.Lerp(ballTransform.position, syncPosition, Time.deltaTime * transformLerpRate);
            //ballTransform.position = syncPosition;
        }
    }

    [Command]
    void CmdProvidePosotionToServer(Vector2 position)
    {
        syncPosition = position;
    }

    [ClientCallback]
    void TranmitPosition()
    {
        if (isServer)
        {
            CmdProvidePosotionToServer(ballTransform.position);
        }
    }

    // Sync Rotation

    private void LerpRotation()
    {

        if (!isLocalPlayer)
        {
            //ballTransform.rotation = Quaternion.Lerp(ballTransform.rotation, syncObjectRotation, Time.deltaTime * rotationLerpRate);
            ballTransform.rotation = syncRotation;
        }

    }

    [Command]
    private void CmdProvideRotationToServer(Quaternion rotation)
    {
        syncRotation = rotation;
    }

    [ClientCallback]
    private void TransmitRotation()
    { // Send rotation to server
        if (isLocalPlayer)
        {
            CmdProvideRotationToServer(ballTransform.rotation);
        }
    }

    //// Sync Rigidbody

    //private void LerpRigidbody()
    //{

    //    if (!isLocalPlayer)
    //    {
    //        //ballTransform.rotation = Quaternion.Lerp(ballTransform.rotation, syncObjectRotation, Time.deltaTime * rotationLerpRate);
    //        GetComponent<Rigidbody2D>().velocity = syncVelocity;
    //    }

    //}

    //[Command]
    //private void CmdProvideRigidbodyToServer(Vector2 velocity)
    //{
    //    syncVelocity = velocity;
    //}

    //[ClientCallback]
    //private void TransmiRigidbody()
    //{ // Send rotation to server
    //    if (isLocalPlayer)
    //    {
    //        CmdProvideRigidbodyToServer(GetComponent<Rigidbody2D>().velocity);
    //    }
    //}





}
