using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    
    public class HealthUI : MonoBehaviour
    {
        public GameObject HeartIconPrefab;
        public HealthChangeIndicator HealthChangeIndicator;
        
        private List<GameObject> _healthIcons = new List<GameObject>();
        
        public void Setup(int maxHealth)
        {
            AddHearts(maxHealth);
        }

        public void SetHealth(int newHealth)
        {
            int iconsCount = _healthIcons.Count;
            if (newHealth > iconsCount)
            {
                AddHearts(newHealth - iconsCount);
                HealthChangeIndicator.ShowHealthChangeEffect(true);
            }
            else if (newHealth < iconsCount) {
                RemoveHearts(iconsCount - newHealth);
                HealthChangeIndicator.ShowHealthChangeEffect(false);
            }
        }
        private void AddHearts(int heartsCount)
        {
            for (int i = 0; i < heartsCount; i++)
            {
                _healthIcons.Add(Instantiate(HeartIconPrefab, transform));
            }
        }
        
        private void RemoveHearts(int heartsCount)
        {
            for (int i = 0; i < heartsCount; i++)
            {
                if (_healthIcons.Count == 0) break;
                GameObject lastIcon = _healthIcons[_healthIcons.Count - 1];
                _healthIcons.Remove(lastIcon);
                Destroy(lastIcon);
            }
        }
        
    }
    
}
