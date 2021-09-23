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
        StartCoroutine(SmoothScale(0.1f, 1f));
    }

    private void Update()
    {
        transform.position +=  transform.forward * Speed * Time.deltaTime;
        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
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
