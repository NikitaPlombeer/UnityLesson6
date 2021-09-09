using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject EffectPrefab;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(EffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}