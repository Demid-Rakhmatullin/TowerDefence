using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarricadeScript : MonoBehaviour
{
    public short HP;

    public Vector3[] DestinationPoints { get; private set; }

    private NavMeshObstacle _navMeshObstacle;

    void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }
    //void Start() { Activate(); }
    public void Activate()
    {
        var shift = GameManager.Instance.CellSize / 2;
        DestinationPoints = new Vector3[4] {
            new Vector3(transform.position.x + shift, transform.position.y, transform.position.z),
            new Vector3(transform.position.x - shift, transform.position.y, transform.position.z),
            new Vector3(transform.position.x, transform.position.y, transform.position.z + shift),
            new Vector3(transform.position.x, transform.position.y, transform.position.z - shift),
        };

        gameObject.tag = GameManager.Instance.BarricadeTag;
        _navMeshObstacle.enabled = true;
    }

    public bool TakeDamage(short damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
