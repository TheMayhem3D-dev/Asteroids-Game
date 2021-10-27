using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameEnd : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private EnemiesSpawner enemiesSpawner;

    [Header("Settings")]
    [SerializeField] private string startScene;
    [SerializeField] private float transitionTime;

    [Header("Events")]
    [SerializeField] private UnityEvent OnVictoryEvent;
    [SerializeField] private UnityEvent OnDefeatEvent;

    private void Start()
    {
        enemiesSpawner.OnEnemiesEnd.AddListener(Victory);
        playerRespawn.OnLivesEndEvent.AddListener(Defeat);
    }

    private void Victory()
    {
        OnVictoryEvent.Invoke();

        StartCoroutine(GoToScene(startScene, transitionTime));
    }

    private void Defeat()
    {
        OnDefeatEvent.Invoke();

        StartCoroutine(GoToScene(startScene, transitionTime));
    }

    IEnumerator GoToScene(string scene, float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(scene);
    }
}
