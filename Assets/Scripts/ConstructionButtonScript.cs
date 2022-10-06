using UnityEngine;

public class ConstructionButtonScript : MonoBehaviour
{
    public GameObject Building;

    public void OnClick()
    {
        if (GameManager.Instance.ConstructingController.PositioningBuildingPrefab != Building)
        {
            GameManager.Instance.ConstructingController.PositioningBuildingPrefab = Building;
        }
        else
        {
            GameManager.Instance.ConstructingController.PositioningBuildingPrefab = null;
        }
    }
}
