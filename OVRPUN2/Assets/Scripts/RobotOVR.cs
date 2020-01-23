using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class RobotOVR : MonoBehaviour {
    
    private static RobotOVR instance = null;
    public GameObject ovrPrefab;
    private PhotonView PV;

    private static int nextId = 400;


    private VRRig _rig;

   
    // Start is called before the first frame update
    void Awake() {
        _rig = GetComponent<VRRig>();

   
        if (instance == null) {
            instance = this;
            Debug.Log("INSTANCE EQUAL TO THIS");
            /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 2f, 0);*/
        }
        else if(instance != this) {
            Debug.Log("DOBLE ROBOT DELETED");
           
            Destroy(_rig);
            return;
        }
        
        PV = GetComponent<PhotonView>();
        if (PV.IsMine) {


            /*
            LazerActivator la = gameObject.AddComponent<LazerActivator>();
            la.visualLazer = gameObject.transform.Find("Lazer").transform.GetChild(0).gameObject;
            la.lazerEventSystem = gameObject.transform.Find("Lazer").transform.GetChild(1).gameObject;
            */

            
            //TODO controversial line of code
            GameObject ovr = Instantiate(ovrPrefab, transform.position, ovrPrefab.transform.rotation);
           
            //TODO check if 200 is taken
           // ovr.GetComponent<PhotonView>().ViewID = nextId;
           // int aux = nextId + 1;
           // PV.RPC("ChangeNextId", RpcTarget.AllBuffered, aux); 

            
            
            _rig = GetComponent<VRRig>();
            /*GameObject ovr = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatarOVR"), 
                transform.position, ovrPrefab.transform.rotation, 0);*/

            string ovrName = ovr.name;

            _rig.head.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;
            _rig.leftHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor").transform;
            _rig.rightHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor").transform;
        }
       
    }

    [PunRPC]
    private void ChangeNextId(int newId) {
        RobotOVR.nextId = newId;
    }
    

}
