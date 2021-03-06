using System;
using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    public float BlinkTime = 1;
    public float BlinkPerSecond = 5;
    public Renderer[] Renderers;

    public void StartBlink()
    {
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        for (float time = 0f; time <= BlinkTime; time += Time.deltaTime)
        {
            float sinParam = time * BlinkPerSecond * Mathf.PI;
            Color emissionColor = new Color(Mathf.Abs(Mathf.Sin(sinParam)), 0f, 0f);
            SetEmissionColor(emissionColor);
            yield return null;
        }

        SetEmissionColor(Color.black);
    }

    private void SetEmissionColor(Color color)
    {
        foreach (Renderer r in Renderers)
        {
            foreach (Material material in r.materials)
            {
                material.SetColor(EmissionColor, color);
            }
        }
    }
}
