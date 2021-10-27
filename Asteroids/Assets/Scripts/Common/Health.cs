using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string[] takeDamageByObjectTags;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("Events")]
    [SerializeField] public UnityEvent onTakeDamageEvent;
    [SerializeField] public UnityEvent OnDieEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out Stats component))
        {
            maxHealth = component.maxHealth;
        }

        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckforDamage(collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckforDamage(collision.collider); 
    }

    private void CheckforDamage(Collider2D collider)
    {
        for (int i = 0; i < takeDamageByObjectTags.Length; i++)
        {
            if (collider.gameObject.tag == takeDamageByObjectTags[i])
            {
                if (TryGetComponent(out Stats stats))
                    SubtractHealth(stats.damage);
                else
                    SubtractHealth(currentHealth);

                onTakeDamageEvent?.Invoke();
                break;
            }
        }
    }

    private void SubtractHealth(int value)
    {
        if ((currentHealth - value) <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= value;
        }
    }

    private void Die()
    {
        OnDieEvent?.Invoke();
    }
}
