using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    public PhotonLobby PhotonLobby;
    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.name.Contains("CustomHand")) return;
        GetComponent<Renderer>().material.color = Color.blue;
        PhotonLobby.BattleCubeTouched();
        
    }

    private void OnCollisionExit(Collision other)
    {
        if(!other.gameObject.name.Contains("CustomHand")) return;
        GetComponent<Renderer>().material.color = Color.green;
    }
}
