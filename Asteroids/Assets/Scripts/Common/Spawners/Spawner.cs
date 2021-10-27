using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected Transform spawnTransform;

    [Header("Events")]
    [SerializeField] protected UnityEvent onSpawnEvent;
    [HideInInspector] public UnityEvent<GameObject> onSpawnObjFromPoolEvent;

    public void Spawn()
    {
        onSpawnEvent?.Invoke();
    }

    public virtual void SpawnObjectFromPool(string tag)
    {
        if (ObjectPooler.Instance != null)
        {
            GameObject go = ObjectPooler.Instance.SpawnFromPool(tag, spawnTransform.position, spawnTransform.rotation);
            onSpawnObjFromPoolEvent.Invoke(go);
        }
        else
        {
            throw new System.Exception("ObjectPooler" + tag + " not found!");
        }
    }
}
