using UnityEngine;

public class BuildableAreaScript : MonoBehaviour
{
    public float BuildingYPosition;
    public string[] AllowedBuildingsTags;

    private bool _hasBuilding;

    public bool HasBuilding { get { return _hasBuilding; } }

    public void SetBuilding(GameObject building)
    {
        var destrScript = building.GetComponent<DestroyableBuildingScript>();
        if (destrScript != null)
            destrScript.Substrate = this;

        _hasBuilding = true;
    }
    
    public void RemoveBuilding()
    {
        _hasBuilding = false;                     
    }
}
