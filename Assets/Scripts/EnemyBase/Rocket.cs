using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float Speed;
    public float RotationSpeed;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.position +=  transform.forward * Speed * Time.deltaTime;
        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
    }
}
