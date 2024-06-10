using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishWithDisplacement : MonoBehaviour
{
    [SerializeField] float _destroyDistance = 10f;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > _destroyDistance)
        {
            GameManager.Instance.RegisterBulletDeath();
            Destroy(this.gameObject);
        }
    }
}
