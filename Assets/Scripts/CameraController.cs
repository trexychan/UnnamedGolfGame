using System;
using Mirror;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 DEFAULT_POS = new Vector3( 21, 12, 6 );
    public Quaternion DEFAULT_ROT = new Quaternion( 0.1788776f, -0.8586125f, 0, 0.4804033f);

    /*
    // Start is called before the first frame update
    private void Awake()
    {
        _PlayerUpdated(ClientScene.localPlayer);

        localPlayerAnnouncer.OnLocalPlayerUpdated += _PlayerUpdated;

    }

    private void OnDestroy()
    {
        localPlayerAnnouncer.OnLocalPlayerUpdated -= _PlayerUpdated;
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (ClientScene.localPlayer)
        {
            this.gameObject.transform.position = ClientScene.localPlayer.gameObject.GetComponent<PlayerScript>().CAMERA_OBJ.transform.position;
            this.gameObject.transform.rotation = ClientScene.localPlayer.gameObject.GetComponent<PlayerScript>().CAMERA_OBJ.transform.rotation;
        }
    }
    /*
    private void _PlayerUpdated(NetworkIdentity localPlayer)
    {
        Debug.Log("NETWORKD HERE");
        if ( localPlayer != null)
        {
            this.gameObject.transform.position = localPlayer.gameObject.GetComponent<PlayerScript>().CAMERA_OBJ.transform.position;
            this.gameObject.transform.rotation = localPlayer.gameObject.GetComponent<PlayerScript>().CAMERA_OBJ.transform.rotation;
        }
        else
        {
            this.gameObject.transform.position = DEFAULT_POS;
            this.gameObject.transform.rotation = DEFAULT_ROT;
        }
    }
    */
}
