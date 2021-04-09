using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CreepLayer;
    public int BuildableLayer;
    public string CreepTag;
    public string BarricadeTag;
    public string WallTag;
    public float BulletLifetime;
    public float CreepRecalculatePathPeriod;
    public float CellSize;
    public float TowerYPosition;
    public float BarricadeYPosition;

    public Transform CreepsTarget;
    public GameObject PositioningTower;
    public GameObject PositioningBarricade;

    void Awake()
    {
        if (Instance == null)
            Instance = gameObject.GetComponent<GameManager>();
    }
}
