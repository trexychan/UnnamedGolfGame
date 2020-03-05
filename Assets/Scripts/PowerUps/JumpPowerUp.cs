using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUp
{
    public string name { get; set; }

    public float JUMP_POWER = 300f;
    public JumpPowerUp()
    {
        this.name = "Jump";
    }
    public void onUse(GameObject ball)
    {
        Debug.Log("here");
        ball.GetComponent<Rigidbody>().AddForce(Vector3.up * JUMP_POWER);
    }

}
