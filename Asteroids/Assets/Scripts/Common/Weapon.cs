using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Spawner spawner;

    [Header("Stats")]
    [SerializeField] private int maxAmmo = 5;
    private int currentAmmo;

    [Header("Stats")]
    [SerializeField] public UnityEvent OnShootEvent;

    private void Awake()
    {
        if (spawner == null)
            spawner = GetComponent<Spawner>();
    }

    private void Start()
    {
        spawner.onSpawnObjFromPoolEvent.AddListener(BulletSpawned);
        currentAmmo = maxAmmo;
    }

    public void ShootAction(InputAction.CallbackContext context)
    {
        OnShootEvent.Invoke();
    }

    public void ShootBullet()
    {
        if (currentAmmo > 0)
        {
            spawner.Spawn();
            currentAmmo--;
        }
    }

    public void BulletSpawned(GameObject bullet)
    {
        Vector2 shootDirection = bullet.transform.position - transform.position;
        bullet.GetComponent<Bullet>().SetUp(this, shootDirection);
    }

    public void BulletReturned()
    {
        if(currentAmmo < maxAmmo)
            currentAmmo++;
    }
}
