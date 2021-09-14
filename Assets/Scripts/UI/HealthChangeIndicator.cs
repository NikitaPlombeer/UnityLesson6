using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthChangeIndicator : MonoBehaviour
    {

        public float IndicationTime = 1;
        private Image _borderImage;
        
        private void Start()
        {
            _borderImage = GetComponent<Image>();
        }


        public void ShowHealthChangeEffect(bool isPositiveEffect)
        {
            StartCoroutine(ShowBorderEffect(isPositiveEffect));
        }
        
        private IEnumerator ShowBorderEffect(bool isPositive)
        {
            _borderImage.enabled = true;
            _borderImage.color = isPositive ? Color.green : Color.red;
            
            for (float time = IndicationTime; time >= 0f; time -= Time.deltaTime)
            {
                Color newColor = _borderImage.color;
                newColor.a = time / IndicationTime;
                _borderImage.color = newColor;
                yield return null;
            }

            _borderImage.enabled = false;
        }
    }
}