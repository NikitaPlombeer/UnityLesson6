using UI;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 5;
    public int MaxHealth = 8;

    public UnityEvent OnTakeDamage;
    public HealthUI UI;
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

        UI.SetHealth(Health);

        OnTakeDamage.Invoke();
        
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
