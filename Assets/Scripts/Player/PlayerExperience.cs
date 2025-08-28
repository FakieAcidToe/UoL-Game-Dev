using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    // Player Experience and Level
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [Header("UI")]
    public Image experienceBar;
    public Text levelText;

    // Class to define level range and the corresponding experience cap increase
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    void Start()
    {
        // Initialise experienceCap as the first experience cap
        experienceCap = levelRanges[0].experienceCapIncrease;
        UpdateExperienceBar();
        UpdateLevelText();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExperienceBar();
    }

    void LevelUpChecker()
    {
        if (experience > experienceCap)
        {
            level++;
            experience = experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            UpdateLevelText();
        }
    }

    void UpdateExperienceBar()
    {
        experienceBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }
}
