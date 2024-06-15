using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int _targetValue = 5;

    public void ApplyDamage()
    {
        GameManager.Instance.AddScore(_targetValue);
        gameObject.SetActive(false);
    }
}
