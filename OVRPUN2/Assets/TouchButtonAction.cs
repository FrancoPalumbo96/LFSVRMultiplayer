using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class TouchButtonAction : MonoBehaviourPun {

    public VideoPlayer videoPlayer;
    private float yOnPos = 1.065f;
    private float yOffPoss = 1.1f;
    private bool isOn = false;
    
    private bool waiting = false;

    private float waitTime = 10f;
    private float timePassed = 10f;

    private void Start() {
        //MyEvents.current.playerEnteredRoom += SyncVideoNewPlayer(videoPlayer.time, videoPlayer.isPlaying);
        MyEvents.current.playerEnteredRoom += PlayerEnteredRoom;

    }

    private void OnTriggerEnter(Collider other) {
        
        if(!other.gameObject.name.Contains("coll_hands:b_l_index2")) return;
        
        if(photonView != null) photonView.RequestOwnership();
        
        if(waiting)return;
        
        if(!photonView.IsMine) return;
        
        StartCoroutine(Wait());
        if (!isOn) {
            GetComponent<Renderer>().material.color = Color.red;
            Vector3 pos = transform.position;
            isOn = true;
            transform.position = new Vector3(pos.x, yOnPos, pos.z);
            videoPlayer.Play();
            SyncVideo(videoPlayer.time, isOn);

        }
        else {
            GetComponent<Renderer>().material.color = Color.green;
            Vector3 pos = transform.position;
            isOn = false;
            transform.position = new Vector3(pos.x, yOffPoss, pos.z);
            videoPlayer.Pause();
            SyncVideo(videoPlayer.time, isOn);

        }


    }

    private IEnumerator Wait() {
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
    }
    
    private IEnumerator MovingButton() {
        //TODO move down
        /*for (int i = 0; i < 1; i++) {
            
            yield return null;
        }*/
        yield return null;
        GetComponent<Renderer>().material.color = Color.red;
    }
    
    private void SyncVideo(double time, bool on) {
        Debug.LogWarning("Sync video executed");

        photonView.RPC("setVideoTime", RpcTarget.AllViaServer, time, on);
        photonView.RPC("setVideoTime", RpcTarget.MasterClient, time, on);

    }

    [PunRPC]
    private void setVideoTime(double time, bool on) {
        if(on)
            videoPlayer.Play();
        else 
            videoPlayer.Pause();

        ChangeButtonStatus(on);
        videoPlayer.time = time;
        isOn = on;
        Debug.LogWarning("Video Time: " + videoPlayer.time);
        Debug.LogWarning("Video Status: " + videoPlayer.isPlaying);

    }
    
    /* 
    }*/

    /*private void Update() {
        if (!base.photonView.IsMine) return;
        timePassed -= Time.deltaTime;
        if (!(timePassed <= 0)) return;
        SyncVideo(videoPlayer.time, videoPlayer.isPlaying);
        timePassed = waitTime;
    }*/

    /*private System.Action SyncVideoNewPlayer(double time, bool on) {
        Debug.LogWarning("Time : "+time+" On: "+on);
        if (time == 0) 
            return null;
        //SyncVideo(time, on);
        return null;
    }*/

    private void PlayerEnteredRoom() {
        Debug.LogWarning("Send rpc was sent");
        photonView.RPC("SendRPC", RpcTarget.MasterClient);   
    }

    [PunRPC]
    private void SendRPC() {
        Debug.LogWarning("Send rpc was executed");
        Debug.LogWarning("Video Time: " + videoPlayer.time);
        Debug.LogWarning("Video Status: " + videoPlayer.isPlaying);
        SyncVideo(videoPlayer.time, videoPlayer.isPlaying);
    }
    
    private void ChangeButtonStatus(bool isPress) {
        if (isPress) {
            GetComponent<Renderer>().material.color = Color.red;
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, yOnPos, pos.z);
        }
        else {
            GetComponent<Renderer>().material.color = Color.green;
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, yOffPoss, pos.z);
        }
    }
    
    
}
