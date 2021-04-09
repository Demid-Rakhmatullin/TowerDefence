using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private GameObject _positioningTower;

    void OnMouseEnter()
    {
        if (GameManager.Instance.PositioningTower != null)
        {
            var position = new Vector3(transform.position.x, GameManager.Instance.TowerYPosition, transform.position.z);
            _positioningTower = Instantiate(GameManager.Instance.PositioningTower, position, Quaternion.identity);
        }
    }

    void OnMouseDown()
    {
        if (_positioningTower != null)
        {
            _positioningTower.GetComponentInChildren<CannonScript>().Activated = true;
            _positioningTower = null;
            GameManager.Instance.PositioningTower = null;
        }            
    }

    void OnMouseExit()
    {
        if (_positioningTower != null)
        {
            Destroy(_positioningTower);
            _positioningTower = null;
        }
    }
}
