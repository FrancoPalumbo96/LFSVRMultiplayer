using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class LazerActivator : MonoBehaviourPun {

    public GameObject visualLazer;
    public GameObject lazerEventSystem;

    private bool isLazerActive = false;

    private PhotonView photonView;
    
    
    // Start is called before the first frame update
    void Start()
    {
//        photonView = transform.parent.parent.GetComponent<PhotonView>();
        /*if (transform.parent == null) {
            this.enabled = false;
        }*/

//        photonView = gameObject.AddComponent<PhotonView>();
        photonView = GetComponent<PhotonView>();
       // photonView.ViewID = 201;
        
//        Debug.LogError(photonView);
        if(!photonView.IsMine) return;

        photonView.RPC("DeactivateLazer", RpcTarget.AllBuffered); 

    }

    private void Update() {
//        Debug.LogError(photonView);
        if(photonView.IsMine)
            LazerManager();
        //photonView.RPC("SendRPC", RpcTarget.MasterClient);   
    }

 
    private void LazerManager() {
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;

        if (isLazerActive) {

            visualLazer.SetActive(false);
            lazerEventSystem.SetActive(false);
            isLazerActive = false;
            Debug.LogWarning("Lazer Deactivated");

            photonView.RPC("DeactivateLazer", RpcTarget.OthersBuffered); 
//                DeactivateLazer();
        }
        else {
            visualLazer.SetActive(true);
            lazerEventSystem.SetActive(true);
            isLazerActive = true;
            Debug.LogWarning("Lazer Ativated");
            photonView.RPC("activateLazer", RpcTarget.OthersBuffered); 
//                ActivateLazer();
        }
        /*if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch)) {
            Debug.LogWarning("Activated Lazer Left");
        }*/
    }

    [PunRPC]
    private void DeactivateLazer() {
        visualLazer.SetActive(false);
        lazerEventSystem.SetActive(false);
        isLazerActive = false;
        Debug.LogWarning("Lazer Deactivated");

    }

    [PunRPC]
    private void activateLazer() {
        visualLazer.SetActive(true);
        lazerEventSystem.SetActive(true);
        isLazerActive = true;
        Debug.LogWarning("Lazer Ativated");

    }

    
    
}
