using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public GameObject Obj;
    public int Count;

    private List<GameObject> _pool;
    
    void Start()
    {
        _pool = new List<GameObject>(Count);
        GameObject tmp;
        for (int i = 0; i < Count; i++)
        {
            tmp = Instantiate(Obj, transform);
            tmp.SetActive(false);
            _pool.Add(tmp);
        }
    }

    public GameObject Get()
    {
        var obj = _pool.FirstOrDefault(o => !o.activeInHierarchy);
        if (obj == null)
            Debug.Log("Object pool of " + Obj.name + " has ended");
           
        return obj;
    }     
}
