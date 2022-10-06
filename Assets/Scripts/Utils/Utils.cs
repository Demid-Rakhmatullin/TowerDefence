using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public static class Utils
{
    public static Transform FindClosest(Vector3 currentPosition, IEnumerable<Transform> objects)
    {
        return objects.Select(t => (Vector3.Distance(t.position, currentPosition), t)).Min().t;
    }

    public static Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
            if (child.CompareTag(tag))
                return child;

        return null;
    }

    public static float CalculatePathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return 0;

        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    }

    public static void MakeTransparent(GameObject obj)
    {
        foreach (var rendered in obj.GetComponentsInChildren<Renderer>())
            foreach (var material in rendered.materials)
            {
                var color = material.color;
                color.a = 0.5f;
                material.color = color;
            }
    }
}

