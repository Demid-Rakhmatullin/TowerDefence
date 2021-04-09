using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private GameObject _positioningBarricade;

    void OnMouseEnter()
    {
        if (GameManager.Instance.PositioningBarricade != null)
        {
            var position = new Vector3(transform.position.x, GameManager.Instance.BarricadeYPosition, transform.position.z);
            _positioningBarricade = Instantiate(GameManager.Instance.PositioningBarricade, position, Quaternion.identity);
        }
    }

    void OnMouseDown()
    {
        if (_positioningBarricade != null)
        {
            _positioningBarricade.GetComponent<BarricadeScript>().Activate();
            _positioningBarricade = null;
            GameManager.Instance.PositioningBarricade = null;
        }
    }

    void OnMouseExit()
    {
        if (_positioningBarricade != null)
        {
            Destroy(_positioningBarricade);
            _positioningBarricade = null;
        }
    }
}
