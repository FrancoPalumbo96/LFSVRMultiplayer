using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SerializationTest : MonoBehaviour, IPunObservable {
    private Rigidbody rigidbody;
    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        
        if (stream.IsWriting)
        {
            stream.SendNext(rigidbody.position);
            stream.SendNext(rigidbody.rotation);
            stream.SendNext(rigidbody.velocity);
        }
        else
        {
            rigidbody.position = (Vector3) stream.ReceiveNext();
            rigidbody.rotation = (Quaternion) stream.ReceiveNext();
            rigidbody.velocity = (Vector3) stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp));
            rigidbody.position += rigidbody.velocity * lag;
        }
    }
}
