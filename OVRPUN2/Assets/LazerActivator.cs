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
        if (transform.parent == null) {
            Destroy(gameObject);
        }
        photonView = GetComponent<PhotonView>();

        if(!photonView.IsMine) return;

        photonView.RPC("DeactivateLazer", RpcTarget.AllBuffered); 

    }

    private void Update() {
        LazerManager();
        //photonView.RPC("SendRPC", RpcTarget.MasterClient);   
    }

 
    private void LazerManager() {
        if(!photonView.IsMine) return;

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
