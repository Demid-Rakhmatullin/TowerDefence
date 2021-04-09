using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingScript : MonoBehaviour
{
    public Camera Camera;
    public GameObject obj;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << GameManager.Instance.BuildableLayer))
            {
                if (hit.transform.CompareTag(GameManager.Instance.WallTag))
                    Instantiate(obj, hit.transform.position + Vector3.up * 3, Quaternion.identity);
            };
        }
    }
}
