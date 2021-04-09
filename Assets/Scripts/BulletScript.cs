using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public short HitDamage { private get;  set; }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GameManager.Instance.CreepTag))
        {
            //Debug.Log("Hit creep");     
            var creepScript = other.gameObject.GetComponent<CreepScript>();
            creepScript.TakeDamage(HitDamage);
        }
        //else
        //{
        //    Debug.Log("Hit smth " + other.gameObject.name);
        //}
        Destroy(gameObject);
    }
}
