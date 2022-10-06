using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public short HitDamage { private get;  set; }

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GameManager.Instance.CreepTag))
        {   
            var creepScript = other.gameObject.GetComponent<CreepScript>();
            creepScript.TakeDamage(HitDamage);
            TakeBackToPool();
        }            
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.BorderTag))
            TakeBackToPool();
    }

    private void TakeBackToPool()
    {
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
        transform.transform.position = Vector3.zero;
    }
}
