using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
public class PhotonPlayer : MonoBehaviour
{

    private PhotonView PV;

    public GameObject myAvatar;
    
    private List<Vector3> spawnPoints;

    public int playerType;
    
    // Start is called before the first frame update
    void Start() {
        
        PV = GetComponent<PhotonView>();



        if (PV.IsMine) {
            //Call RPC that calls another RPC
            Debug.LogWarning("Called Event from photon player");
            MyEvents.current.onPlayerEnteredRoom();
        }
        
        /*MyEvents.current.onPlayerEnteredRoom();
        Debug.LogWarning("Message should print to everyone -> possibility on sending event");*/
        if (PV.IsMine)
        {
            PhotonRoom.AddPlayer();           
            spawnPoints = PhotonRoom.room.spawnPoints;
            Debug.Log(spawnPoints[PhotonRoom.players-1]);
            
            Debug.Log(PhotonRoom.room.spawnPoints.Count);
            
            int spawnPicker = Random.Range(0, spawnPoints.Count);

            switch (playerType) {
                case 0:
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot"/*"PlayerAvatarOVR"*/), 
                        spawnPoints[spawnPicker], Quaternion.identity, 0);
                    Debug.Log("Robot was chosen, playerType = " + playerType);
                    break;
                case 1:
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ghost"/*"PlayerAvatarOVR"*/), 
                        spawnPoints[spawnPicker], Quaternion.identity, 0);
                    Debug.Log("Ghost was chosen, playerType = " + playerType);
                    break;
                default:
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot"/*"PlayerAvatarOVR"*/), 
                        spawnPoints[spawnPicker], Quaternion.identity, 0);
                    Debug.Log("Robot was chosen, playerType = " + playerType);
                    break;
            }
            /*myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot"/*"PlayerAvatarOVR"#1#), 
                spawnPoints[spawnPicker], Quaternion.identity, 0);*/
        }
        Debug.Log("Numb of Players: "+PhotonRoom.players);
    }
}
