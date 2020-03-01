using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
    public PowerUp[] powerTypes { get; set; }


    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    public void PickUp()
    {
        Debug.Log("Power up is picked up");
        Destroy(this.gameObject);
    }

    public PowerUp GetPowerUp ()
    {
        return powerTypes[Random.Range(0, powerTypes.Length)];
    }
}
