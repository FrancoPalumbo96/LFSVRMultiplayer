
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerPUN : MonoBehaviourPun {
    private VideoPlayer videoPlayer;
    
    // Start is called before the first frame update
    void Start() {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += SyncVideo;
    }
    
    
    private void SyncVideo(VideoPlayer vp) {
        photonView.RPC("setVideoTime", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void setVideoTime() {
        videoPlayer.time = 0;
        Debug.Log("Video Time: " + videoPlayer.time);
    }

    
}
