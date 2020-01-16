using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class RobotOVR : MonoBehaviour {
    
    private static RobotOVR instance = null;
    public GameObject ovrPrefab;
    private PhotonView PV;


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

            GameObject ovr = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatarOVR"),
                transform.position, ovrPrefab.transform.rotation);
            //GameObject ovr = Instantiate(ovrPrefab, transform.position, ovrPrefab.transform.rotation);
            _rig = GetComponent<VRRig>();

            string ovrName = ovr.name;

            _rig.head.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;
            _rig.leftHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor").transform;
            _rig.rightHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor").transform;
        }
       
    }

}
