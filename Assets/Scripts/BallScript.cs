using UnityEngine;
using System;
using Mirror;
public class BallScript : NetworkBehaviour

{
    public GameObject ENTITY;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision: " + collider.gameObject.name + collider.gameObject.tag) ;
        if(collider.gameObject.tag == "goal")
        {
            Debug.Log("reached goal");
            ENTITY.GetComponent<PlayerScript>().reachedGoal();
        }
    }
}
