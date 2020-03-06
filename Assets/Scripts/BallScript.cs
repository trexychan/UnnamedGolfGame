using UnityEngine;
using System;
using Mirror;
public class BallScript : NetworkBehaviour

{
    public GameObject ENTITY;
    public float coolDownTimer = 0;

    private void Start()
    {
        this.gameObject.tag = "ball";
    }

    private void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision: " + collider.gameObject.name + collider.gameObject.tag) ;
        if(collider.gameObject.tag == "goal")
        {
            Debug.Log("reached goal");
            ENTITY.GetComponent<PlayerScript>().reachedGoal();
        }
        else if (collider.gameObject.tag == "PowerUp")
        {
            Debug.Log("hit power up!");
            PowerUp powerUpType = collider.gameObject.GetComponent<PowerUpController>().GetPowerUp();
            collider.gameObject.GetComponent<PowerUpController>().PickUp();
            ENTITY.GetComponent<PlayerScript>().pickedUpPowerUp(powerUpType);
        }
        // Sabin Kim: Apply greater force upon touching SpeedUp pad
        else if (collider.gameObject.tag == "SpeedUp")
        {
            Debug.Log("speeding up");
            Quaternion speedupRotation = collider.gameObject.transform.rotation;
            // for now, assume SpeedUp pads are parallel to xz-plane
            Vector3 force = new Vector3((float) (-1 * Math.Cos(speedupRotation.y)),
                                        0, 
                                        (float) Math.Sin(speedupRotation.y));
            force *= 7;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Debug.Log("DEATH!");
            ENTITY.GetComponent<PlayerScript>().enterDeathZone();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Debug.Log("DEATH!");
            ENTITY.GetComponent<PlayerScript>().exitDeathZone();
        }
    }
}
