using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class Explodable : MonoBehaviour, IDamageable
{
    private bool _exploding = false;
    private bool _effectPlayed = false;

    [SerializeField] private float _explosionDelay = 0.1f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _upwardModifier = 2f;
    [SerializeField] GameObject _explosionEffect;

    public void TriggerExplosion()
    {
        if (!_exploding)
        {
            _exploding = true;
            StartCoroutine(PerformExplosion());
        }
    }

    IEnumerator PerformExplosion()
    {
        yield return new WaitForSeconds(_explosionDelay);

        // Explosion particle effect
        if (_explosionEffect && !_effectPlayed) { 
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            _effectPlayed = true;
        }

        // Apply explosion force to everything in the explosion radius
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in affectedObjects)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.ApplyDamage();
            }

            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardModifier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    public void ApplyDamage()
    {
        TriggerExplosion();
    }
}
