using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CannonScript : MonoBehaviour
{
    public float ShootSpeed;
    public short Damage;

    public bool Activated;
    public float Range;

    public Rigidbody Bullet;
    public float BulletSpeed;
    public float BulletStartShift;

    private float _lastShootTime;

    void FixedUpdate()
    {
        if (Activated)
        {
            var creeps = Physics.OverlapSphere(transform.position, Range, 1 << GameManager.Instance.CreepLayer);
            if (creeps.Any())
            {
                var closest = Utils.FindClosest(transform.position, creeps.Select(c => c.transform));
                transform.LookAt(closest.transform);
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.fixedTime - _lastShootTime >= 1 / ShootSpeed)
        {
            var bulletPosition = transform.forward * BulletStartShift + transform.position;
            var clone = Instantiate(Bullet, bulletPosition, Quaternion.identity);
            clone.GetComponent<BulletScript>().HitDamage = Damage;
            clone.gameObject.SetActive(true);
            clone.AddForce(transform.forward * BulletSpeed, ForceMode.VelocityChange);
            Destroy(clone.gameObject, GameManager.Instance.BulletLifetime);
            _lastShootTime = Time.fixedTime;
        }
    }
}
