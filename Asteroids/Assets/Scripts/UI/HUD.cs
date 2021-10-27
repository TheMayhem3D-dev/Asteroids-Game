using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] private List<Image> lifeSprites;

    public void UpdateLifeSprites(int value)
    {
        for (int i = 0; i < lifeSprites.Capacity; i++)
        {
            if(i <= (value-1))
                lifeSprites[i].enabled = true;
            else
                lifeSprites[i].enabled = false;
        }
    }
}
