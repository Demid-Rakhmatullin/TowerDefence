using UnityEngine;
using System.Linq;

public class ConstructingController : MonoBehaviour
{
    public Camera Camera;
    public GameObject PositioningBuildingPrefab;

    private GameObject _positioningBuildingInstance;
    private GameObject _lastPrefab;
   
    void Start()
    {
        InitStaticBuildings();
    }


    void Update()
    {
        if (PositioningBuildingPrefab != null)
        {
            if (IsProperPlaceForBuilding(PositioningBuildingPrefab, out BuildableAreaScript area))
            {
                if (Input.GetMouseButtonDown(0))
                    ConstructBuilding(PositioningBuildingPrefab, area);
                else
                    LocateBuilding(PositioningBuildingPrefab, area);
            }
            else if (_positioningBuildingInstance != null)
            {
                ResetPositioningInstance();
            }

        }
    }

    private bool IsProperPlaceForBuilding(GameObject buildingPrefab, out BuildableAreaScript area)
    {
        var ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << GameManager.Instance.BuildableLayer))
        {
            area = hit.transform.GetComponent<BuildableAreaScript>();
            return !area.HasBuilding 
                && area.AllowedBuildingsTags.Contains(buildingPrefab.tag);
        }
        area = null;
        return false;
    }

    private void LocateBuilding(GameObject buildingPrefab, BuildableAreaScript area)
    {
        if (_positioningBuildingInstance == null || _lastPrefab != buildingPrefab)
        {
            _positioningBuildingInstance = Instantiate(buildingPrefab, GetConstructionPos(area), Quaternion.identity);
            Utils.MakeTransparent(_positioningBuildingInstance);
            _lastPrefab = buildingPrefab;
        }
        else if (IsDifferentArea(_positioningBuildingInstance.transform.position, area))
            _positioningBuildingInstance.transform.position = GetConstructionPos(area);
    }
   
    private void ConstructBuilding(GameObject buildingPrefab, BuildableAreaScript area)
    {
        var building = Instantiate(buildingPrefab, GetConstructionPos(area), Quaternion.identity, transform);
        area.SetBuilding(building);
        building.GetComponent<NeedActivationScript>()?.Activate();
        ResetPositioningInstance();
        PositioningBuildingPrefab = null;
    }

    private void ResetPositioningInstance()
    {
        Destroy(_positioningBuildingInstance);
        _positioningBuildingInstance = null;
    }

    private Vector3 GetConstructionPos(BuildableAreaScript area)
        => new Vector3(area.transform.position.x, area.BuildingYPosition, area.transform.position.z);

    private bool IsDifferentArea(Vector3 currentPosition, BuildableAreaScript area)
        => currentPosition.x != area.transform.position.x || currentPosition.z != area.transform.position.z;

    private void InitStaticBuildings()
    {
        foreach (var groundCell in GameObject.FindGameObjectsWithTag(GameManager.Instance.GroundTag))
            foreach (var nearbyObject in Physics.OverlapSphere(groundCell.transform.position, GameManager.Instance.StaticBuildingsDetectionRadius))
                if (nearbyObject.CompareTag(GameManager.Instance.StaticBuildingTag))
                {
                    groundCell.GetComponent<BuildableAreaScript>().SetBuilding(nearbyObject.gameObject);
                    break;
                }
    }
}
