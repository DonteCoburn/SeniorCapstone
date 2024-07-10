using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This code controls each player's camera, normally this is easy, but had to be heavily modified for the spectate system.

public class MultiplayerCameraFollow : MonoBehaviourPun
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float yOffset = 1f;
    private bool targetSet = false;

    //It checks all the time, where if it doesn't have anything to follow it tries for a target
    // Then if it has a target it, it follows them. I took this code from our exisiting camera's code, so thanks whoever wrote that
    void Update()
    {
        if (!targetSet)
        {
            TrySetTarget();
        }
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }

    //This code is ran when the button is pressed, ran directly by SpectateSystem, check that script for more info
    public void SetTarget(int actorNumber)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PhotonView view = player.GetComponent<PhotonView>();
            if (view != null && view.Owner.ActorNumber == actorNumber)
            {
                target = player.transform;
                targetSet = true;
                return;
            }
        }
    }

    //This is only ran when we have no initial target, useful for finding our own player
    private void TrySetTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player && player.GetComponent<PhotonView>().IsMine)
        {
            target = player.transform;
            targetSet = true;
        }
    }
}