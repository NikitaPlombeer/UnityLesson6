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
        Die();
    }

    public void Die()
    {
        Instantiate(EffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}