using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ActivateNearbyTarget : MonoBehaviour
{
    public float ActivationRadius;
    public Transform Target;
    public GameObject[] Objects;


    private void OnDrawGizmosSelected()
    {
        if (Target == null) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawSphere(Target.position, ActivationRadius);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in Objects)
        {
            if (obj == null) continue;

            obj.SetActive(Vector3.Distance(Target.position, obj.transform.position) <= ActivationRadius);
        }
    }
}