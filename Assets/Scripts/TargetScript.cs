using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int _targetValue = 5;
    [SerializeField] private float _disappearDelay = 2f;

    //private void OnDestroy()
    //{
    //    GameManager.Instance.CheckWinCondition();
    //}

    public void ApplyDamage()
    {
        GameManager.Instance.AddScore(_targetValue);
        if (transform.GetComponentInChildren<CharacterJoint>() && TryGetComponent<Animator>(out Animator animator))
        {
            // We think it's a ragdoll and it has an animator
            animator.enabled = false;
            Destroy(gameObject, _disappearDelay);
        }
    }
}
