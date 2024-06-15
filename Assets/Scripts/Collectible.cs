using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _collectibleValue = 2;
    [SerializeField] private float _spinSpeed = 45f;

    public void Collect()
    {
        GameManager.Instance.AddScore(_collectibleValue);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _spinSpeed);
    }
}
