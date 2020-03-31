using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProofPowerUp : PowerUp
{
    public string name { get; set; }

    public float JUMP_POWER = 300f;
    public FireProofPowerUp()
    {
        this.name = "FireProof";
    }
    public void onUse(GameObject ball)
    {
        Debug.Log("The fire can't stop me!");
    }

}
