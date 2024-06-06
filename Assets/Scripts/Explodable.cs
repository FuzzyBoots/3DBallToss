using System.Collections;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    private bool _exploding = false;
    [SerializeField] private float _explosionDelay = 0.1f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _upwardModifier = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TriggerExplosion();
        }
    }

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

        // Apply explosion force to everything in the explosion radius
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in affectedObjects)
        {
            if (collider.TryGetComponent<Explodable>(out Explodable exploder))
            {
                exploder.TriggerExplosion();
            }

            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardModifier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
