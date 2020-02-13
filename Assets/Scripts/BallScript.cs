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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            Debug.Log("hit power up!");
            PowerUp powerUpType = collision.gameObject.GetComponent<PowerUpController>().GetPowerUp();
            collision.gameObject.GetComponent<PowerUpController>().PickUp();
            ENTITY.GetComponent<PlayerScript>().pickedUpPowerUp(powerUpType);
        }
    } 
}
