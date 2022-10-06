using UnityEngine;
using System.Linq;

public class TowerScript : MonoBehaviour
{
    public float ShootSpeed;
    public short Damage;

    public float Range;

    public string BulletsPoolTag;
    public float BulletSpeed;
    public float BulletStartShift;
    public float BulletStartHeight;

    private float _lastShootTime;
    private NeedActivationScript _activationScript;
    private Transform _cannon;
    private ObjectsPool _bulletsPool;

    void Awake()
    {
        _activationScript = GetComponent<NeedActivationScript>();
    }

    void Start()
    {
        _cannon = Utils.FindChildWithTag(transform, GameManager.Instance.CannonTag);
        _bulletsPool = GameObject.FindGameObjectWithTag(BulletsPoolTag).GetComponent<ObjectsPool>();
    }

    void FixedUpdate()
    {
        if (_activationScript.Activated)
        {
            var creeps = Physics.OverlapSphere(transform.position, Range, 1 << GameManager.Instance.CreepLayer);
            if (creeps.Any())
            {
                var closest = Utils.FindClosest(transform.position, creeps.Select(c => c.transform));
                _cannon.LookAt(closest.transform);
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.fixedTime - _lastShootTime >= 1 / ShootSpeed)
        {
            var bulletPosition = _cannon.forward * BulletStartShift + new Vector3(transform.position.x, transform.position.y + BulletStartHeight, transform.position.z);
            var bullet = _bulletsPool.Get();
            bullet.transform.position = bulletPosition;
            bullet.GetComponent<BulletScript>().HitDamage = Damage;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(_cannon.forward * BulletSpeed, ForceMode.VelocityChange);
            
            _lastShootTime = Time.fixedTime;
        }
    }
}
