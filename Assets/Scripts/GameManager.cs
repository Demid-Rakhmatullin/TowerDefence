using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CreepLayer;
    public int BuildableLayer;
    public int ObstacleLayer;
    public string CreepTag;
    public string CannonTag;
    public string StaticBuildingTag;
    public string GroundTag;
    public string BorderTag;
    public float BulletLifetime;
    public float CreepRecalculatePathPeriod;
    public float CellSize;
    public float PlayboardDiagonal;
    public float StaticBuildingsDetectionRadius;

    public Transform CreepsTarget;

    public ConstructingController ConstructingController;
    public NavMeshController NavMeshController;

    void Awake()
    {
        if (Instance == null)
            Instance = gameObject.GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
