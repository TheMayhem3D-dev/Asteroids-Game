using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Posiiton")]
    [SerializeField] private RandomPosition randomPosition;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Movement playerMovement;

    [Header("Lives")]
    [SerializeField] private int maxLives;
    private int currentLives;

    [Header("Enemies check")]
    [SerializeField] private CheckForOtherCollider checkForOther = new CheckForOtherCollider();
    [SerializeField] private float checkForOtherRadius;
    [SerializeField] private LayerMask enemiesLayerMask;

    [Header("UI")]
    [SerializeField] private HUD hud;

    [Header("Events")]
    [SerializeField] public UnityEvent OnLivesEndEvent;

    private void Awake()
    {
        currentLives = maxLives;

        if(checkForOther == null)
            checkForOther = new CheckForOtherCollider();

        if (playerMovement == null)
            GetComponent<Movement>();

        if (hud == null)
            hud = GetComponentInChildren<HUD>();
    }

    private void Start()
    {
        hud.UpdateLifeSprites(currentLives);
    }

    public void Respawn()
    {
        if (currentLives > 1)
        {
            Vector2 randomPos = randomPosition.GetRandomPosition();
            if (checkForOther.CircleCheck(randomPos, checkForOtherRadius, enemiesLayerMask))
            {
                playerPosition.position = randomPosition.GetRandomPosition();
                playerMovement.Rb2d.velocity = Vector2.zero;
                currentLives--;
            }
            else
            {
                Respawn();
            }
        }
        else
        {
            OnLivesEndEvent.Invoke();
        }

        hud.UpdateLifeSprites(currentLives);
    }
}
