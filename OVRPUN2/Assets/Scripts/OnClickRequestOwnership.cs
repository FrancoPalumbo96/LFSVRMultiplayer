using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OnClickRequestOwnership : MonoBehaviourPun {

    public void OnClick() {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            Vector3 colVector =
                new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            this.photonView.RPC("ColorRpc", RpcTarget.AllBufferedViaServer, colVector);
        }
        else {
            if (photonView.Owner.UserId.CompareTo(PhotonNetwork.LocalPlayer.UserId) == 0) {
                Debug.Log("Not requesting ownership. Already mine.");
                return;
            }

            photonView.RequestOwnership();
        }
    }

    [PunRPC]
    public void ColorRpc(Vector3 col) {
        Color color = new Color(col.x, col.y, col.z);
        this.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
