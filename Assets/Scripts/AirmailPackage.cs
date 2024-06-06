using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AirmailPackage : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("No Rigidbody found");
            Debug.Break();
        } 
    }

    public void ApplyImpulse(Vector3 force)
    {
        if (_rb != null)
        {
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
