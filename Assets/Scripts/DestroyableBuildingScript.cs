using UnityEngine;

public class DestroyableBuildingScript : MonoBehaviour
{
    public BuildableAreaScript Substrate { get; set; }

    void OnDestroy()
    {
        if (Substrate != null)
            Substrate.RemoveBuilding();
    }
}
