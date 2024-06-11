using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishAfterTime : MonoBehaviour
{
    [SerializeField] float _delayTime = 10f;
    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_delayTime);
        Debug.Log(GameManager.Instance);
        GameManager.Instance.RegisterBulletDeath();
        Destroy(this.gameObject);
    }
}
