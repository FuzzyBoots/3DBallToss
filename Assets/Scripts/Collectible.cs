using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile")) {
            Debug.Log("Collected coin");
            Destroy(this.gameObject);
        }
    }
}
