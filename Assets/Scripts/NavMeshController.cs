using System;
using System.Collections;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    public event Action OnObstacleChange;

    public void InvokeObstacleChange()
    {
        StartCoroutine(InvokeEvent());
    }

    private IEnumerator InvokeEvent()
    {
        //ждем следующего кадра, т.к. NavMesh учитывает изменения в obstacles только в следующем кадре
        yield return null;
        OnObstacleChange?.Invoke();
    }
}
