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
    
    public void Activate()
    {
        var shift = GameManager.Instance.CellSize / 2;
        DestinationPoints = new Vector3[4] {
            new Vector3(transform.position.x + shift, transform.position.y, transform.position.z),
            new Vector3(transform.position.x - shift, transform.position.y, transform.position.z),
            new Vector3(transform.position.x, transform.position.y, transform.position.z + shift),
            new Vector3(transform.position.x, transform.position.y, transform.position.z - shift),
        };

        gameObject.layer = GameManager.Instance.ObstacleLayer;
        _navMeshObstacle.enabled = true;
        //GameManager.Instance.NavMeshController.InvokeObstacleChange();
    }

    public bool TakeDamage(short damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
            //GameManager.Instance.NavMeshController.InvokeObstacleChange();
            return true;
        }
        return false;
    }
}
