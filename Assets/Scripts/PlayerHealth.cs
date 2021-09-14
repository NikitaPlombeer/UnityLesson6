using System;
using UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 5;
    public int MaxHealth = 8;

    public HealthUI UI;
    public Blink blinker;
    
    public AudioSource TakeDamageSound;
    public AudioSource AddHealthSound;
    
    private bool _invulnerable = false;


    private void Start()
    {
        UI.Setup(Health);
    }

    public void TakeDamage(int damageValue)
    {
        if (_invulnerable) return;
      

        Health -= damageValue;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }

        TakeDamageSound.Play();
        UI.SetHealth(Health);
        blinker.StartBlink();
        
        MakeInvulnerableFor(1f);
    }

    private void MakeInvulnerableFor(float time)
    {
        _invulnerable = true;
        Invoke(nameof(StopInvulnerable), time);
    }

    void StopInvulnerable()
    {
        _invulnerable = false;
    }
    
    public void AddHealth(int healthValue)
    {
        Health += healthValue;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        AddHealthSound.Play();
        UI.SetHealth(Health);
    }

    private void Die()
    {
        Debug.Log("You lose");
    }
}
