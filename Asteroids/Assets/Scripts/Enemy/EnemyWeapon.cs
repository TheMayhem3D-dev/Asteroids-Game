using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    [Header("General")]
    [SerializeField] private float minFireRate;
    [SerializeField] private float maxFireRate;
    
    private void OnEnable()
    {
        StartCoroutine(WeaponShootAfterTime());   
    }

    IEnumerator WeaponShootAfterTime()
    {
        OnShootEvent.Invoke();
        yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));

        StartCoroutine(WeaponShootAfterTime());

    }
}
