using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class SyncVidos : MonoBehaviourPun, IPunObservable {
    private VideoPlayer videoPlayer;

    private void Start() {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            
            stream.SendNext(videoPlayer.time);
        }else if (stream.IsReading) {
            if(base.photonView.IsMine)
            videoPlayer.time = (double)stream.ReceiveNext();
        }
    }
       
}
