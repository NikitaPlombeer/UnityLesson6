using System;
using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float Speed;
    public float RotationSpeed;
    public float RocketScaleTime = 1f;
    
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y, 0f);
        var rotationEulerAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(rotationEulerAngles.x, -90f, rotationEulerAngles.z));
        StartCoroutine(SmoothScale(0.1f, 1f));
    }

    private void Update()
    {
        
        Vector3 translatePosition =  transform.forward * Speed * Time.deltaTime;
        translatePosition.z = 0f;
        transform.position += translatePosition;
        
        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
        Vector3 newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(newRotation.x, -90f, newRotation.z));
    }

    private IEnumerator SmoothScale(float startScale, float endScale)
    {
        transform.localScale = new Vector3(startScale, startScale, startScale);
        for (float t = 0; t < RocketScaleTime; t += Time.deltaTime)
        {
            float newScale = Mathf.Lerp(startScale, endScale, t / RocketScaleTime);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            yield return null;
        }
        transform.localScale = new Vector3(endScale, endScale, endScale);
    }
}
