using System;
using UnityEngine;
using System.Collections;
using Photon.Pun;


[RequireComponent( typeof( PhotonView ) )]
public class MaterialPerOwner : MonoBehaviourPun
{
    private string assignedColorForUserId;

    Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        /*if( this.photonView.Owner.UserId != assignedColorForUserId )
        {
            int index = System.Array.IndexOf(ExitGames.UtilityScripts.PlayerRoomIndexing.instance.PlayerIds, this.photonView.ownerId);
            try
            {
                m_Renderer.material.color = FindObjectOfType<ColorPerPlayer>().Colors[index];
                this.assignedColorForUserId = this.photonView.Owner.UserId;
            }
            catch (Exception e)
            {
                //nothing
            }
           
            //Debug.Log("Switched Material to: " + this.assignedColorForUserId + " " + this.renderer.material.GetInstanceID());
        }*/
    }
}