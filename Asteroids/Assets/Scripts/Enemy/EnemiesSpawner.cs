using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawner : Spawner
{
    [Header("Posiiton")]
    [SerializeField] private RandomPosition randomPosition;

    [Header("Enemies start")]
    [SerializeField] private float timeToFirstSpawn;
    [SerializeField] private int startEnemiesCount;

    [Header("Enemies Spawn")]
    [SerializeField] private int enemiesLeftToSpawn;
    [SerializeField] private int enemiesSpawnInOneTime;
    [SerializeField] private float timeToNextSpawn;
    [SerializeField] private float timeSubtract;

    [Header("Enemies check")]
    [SerializeField] private CheckForOtherCollider checkForOther = new CheckForOtherCollider();
    [SerializeField] private float checkForOtherRadius;
    [SerializeField] private LayerMask enemiesLayerMask;

    [Header("Events")]
    [SerializeField] public UnityEvent OnEnemiesEnd;

    private void Awake()
    {
        if (checkForOther == null)
        {
            checkForOther = new CheckForOtherCollider();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies(timeToFirstSpawn, startEnemiesCount));
    }

    IEnumerator SpawnEnemies(float t, int count)
    {
        yield return new WaitForSeconds(t);

        for (int i = 0; i < count; i++)
        {
            onSpawnEvent?.Invoke();
        }
    }

    public override void SpawnObjectFromPool(string tag)
    {
        if (ObjectPooler.Instance != null)
        {
            Vector2 randomPos = randomPosition.GetRandomPosition();
            if (checkForOther.CircleCheck(randomPos, checkForOtherRadius, enemiesLayerMask))
            {
                GameObject go = ObjectPooler.Instance.SpawnFromPool(tag, randomPos, Quaternion.identity);

                Health goHealth = go.GetComponent<Health>();
                goHealth.OnDieEvent.RemoveListener(EnemyDestroyed);
                goHealth.OnDieEvent.AddListener(EnemyDestroyed);

                onSpawnObjFromPoolEvent.Invoke(go);
            }
            else
            {
                SpawnObjectFromPool(tag);
            }
        }
        else
        {
            throw new System.Exception("ObjectPooler" + tag + " not found!");
        }
    }

    public void EnemyDestroyed()
    {
        if ((enemiesLeftToSpawn - enemiesSpawnInOneTime) >= 0)
        {
            StartCoroutine(SpawnEnemies(timeToNextSpawn, enemiesSpawnInOneTime));
            enemiesLeftToSpawn -= enemiesSpawnInOneTime;
            timeToNextSpawn -= timeSubtract;
        }
        else
        {
            OnEnemiesEnd.Invoke();
        }
    }
}
