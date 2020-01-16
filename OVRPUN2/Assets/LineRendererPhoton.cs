using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LineRendererPhoton : MonoBehaviourPun {
    private LineRenderer lineRenderer;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Start() {
        photonView = transform.parent.parent.GetComponent<PhotonView>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    [PunRPC]
    private void updateLineRenderer(Vector3 pos1, Vector3 pos2) {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }
    
    
    private void Update() {
        photonView.RPC("updateLineRenderer", RpcTarget.Others, lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));   
    }


    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(lineRenderer);
            
        }
        else
        {
            // Network player, receive data
            lineRenderer = (LineRenderer)stream.ReceiveNext();
        }
    }*/
}
