using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerExperience : MonoBehaviour
{
	// Player Experience and Level
	[Header("Experience/Level")]
	[SerializeField] int experience = 0;
	[SerializeField] int level = 1;
	[SerializeField] int experienceCap;

	[Header("UI")]
	[SerializeField] Image experienceBar;
	[SerializeField] Text levelText;

	[Header("Events")]
	[SerializeField] UnityEvent onLevelUp;

	// Class to define level range and the corresponding experience cap increase
	[System.Serializable]
	class LevelRange
	{
		public int startLevel;
		public int endLevel;
		public int experienceCapIncrease;
	}

	[SerializeField] List<LevelRange> levelRanges;

	float expMultiplier = 1; // for PlayerStatus.Instance.playerEXP

	void Start()
	{
		// Initialise experienceCap as the first experience cap
		experienceCap = levelRanges[0].experienceCapIncrease;
		UpdateExperienceBar();
		UpdateLevelText();

		if (PlayerStatus.Instance != null)
			expMultiplier *= PlayerStatus.Instance.playerEXP;
	}

	public void IncreaseExperience(int amount)
	{
		experience += amount;
		LevelUpChecker();
		UpdateExperienceBar();
	}

	void LevelUpChecker()
	{
		if (experience > GetScaledExpCap())
		{
			level++;
			experience = 0;
			//experience = experienceCap;

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

			onLevelUp.Invoke();
		}
	}

	int GetScaledExpCap()
	{
		return Mathf.CeilToInt(experienceCap / expMultiplier);
	}

	void UpdateExperienceBar()
	{
		experienceBar.fillAmount = (float)experience / GetScaledExpCap();
	}

	void UpdateLevelText()
	{
		levelText.text = "LV " + level.ToString();
	}
}
