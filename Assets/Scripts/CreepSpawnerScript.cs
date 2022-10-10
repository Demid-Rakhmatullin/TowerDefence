using System.Collections;
using UnityEngine;

public class CreepSpawnerScript : MonoBehaviour
{
    public GameObject[] Creeps;
    public float SpawnPeriod;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var index = Random.Range(0, 3);
            Instantiate(Creeps[index], transform.position, Quaternion.identity, transform);
            
            yield return new WaitForSeconds(SpawnPeriod);
        }
    }
}
