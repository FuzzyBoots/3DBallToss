using UnityEngine;

class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damaged))
        {
            damaged.ApplyDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Collectible>(out Collectible collectible))
        {
            collectible.Collect();
        }
    }
}