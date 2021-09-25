using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchPrefabCreator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform[] Spawns;

    [ContextMenu("Create")]
    public void Create()
    {
        foreach (Transform spawn in Spawns)
        {
            Instantiate(Prefab, spawn.position, spawn.rotation);
        }
    }
}
