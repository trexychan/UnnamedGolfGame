using UnityEngine;
using System;
using Mirror;
public class BallScript : NetworkBehaviour
{
    public GameObject ENTITY;
    public float SPEED_UP_SPEED = 1.5f;
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
            

            Vector3 ball_direction = GetComponent<Rigidbody>().velocity / GetComponent<Rigidbody>().velocity.magnitude;
            GetComponent<Rigidbody>().AddForce( ball_direction * SPEED_UP_SPEED, ForceMode.Impulse);
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
