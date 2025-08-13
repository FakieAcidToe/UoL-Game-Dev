using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerExperience player = FindObjectOfType<PlayerExperience>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
