using UnityEngine;

public class DestroyableBuildingScript : MonoBehaviour
{
    [ReadOnly]
    public BuildableAreaScript Substrate;

    void OnDestroy()
    {
        if (Substrate != null)
            Substrate.RemoveBuilding();
    }
}
