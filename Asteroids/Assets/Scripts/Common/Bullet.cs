using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Movement movement;
    [SerializeField] private Health health;
    private Weapon weapon;
    private Vector3 shootDirection;

    private void Awake()
    {
        if (movement == null)
            movement = GetComponent<Movement>();

        if (health == null)
            health = GetComponent<Health>();
    }

    public void SetUp(Weapon owner, Vector3 vector)
    {
        weapon = owner;

        health.OnDieEvent.RemoveListener(weapon.BulletReturned);
        health.OnDieEvent.AddListener(weapon.BulletReturned);

        shootDirection = vector;
    }

    private void FixedUpdate()
    {
        movement.MoveTo(shootDirection);
    }
}
