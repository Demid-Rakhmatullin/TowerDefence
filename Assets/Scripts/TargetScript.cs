using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.CreepTag))
        {
            Destroy(other.gameObject);
        }
    }
}
