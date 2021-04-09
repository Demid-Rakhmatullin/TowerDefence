using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Camera Camera;

    public Button BarricadeButton;
    public GameObject Barricade;

    public Button StandartTowerButton;
    public GameObject StandartTower;

    public Button FastTowerButton;
    public GameObject FastTower;

    public Button HeavyTowerButton;
    public GameObject HeavyTower;

    void Start()
    {
        BarricadeButton.onClick.AddListener(OnBarricadeClick);
        StandartTowerButton.onClick.AddListener(OnStandartTowerClick);
        FastTowerButton.onClick.AddListener(OnFastTowerClick);
        HeavyTowerButton.onClick.AddListener(OnHeavyTowerClick);
    }

    void OnBarricadeClick()
    {
        GameManager.Instance.PositioningBarricade = Barricade;
    }

    void OnStandartTowerClick()
    {
        GameManager.Instance.PositioningTower = StandartTower;      
    }

    void OnFastTowerClick()
    {
        GameManager.Instance.PositioningTower = FastTower;
    }

    void OnHeavyTowerClick()
    {
        GameManager.Instance.PositioningTower = HeavyTower;
    }
}
