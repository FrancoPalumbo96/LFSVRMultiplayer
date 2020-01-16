using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun
{

    private OVRGrabbable _ovrGrabbable;
    private bool calledTransfer;
    
    private void Start()
    {
        _ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    //Change to events
    private void Update()
    {
        if (_ovrGrabbable.isGrabbed)
        {
            if (calledTransfer) return;
            calledTransfer = true;
            Transfer();
        }
        else
        {
            calledTransfer = false;
        }
    }

    public void Transfer() {
        
        base.photonView.RequestOwnership();
    }
    
}
