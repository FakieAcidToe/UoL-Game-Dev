using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
	public static DamageNumberSpawner Instance { private set; get; }
	[SerializeField] DamageNumberCanvas damageNumberPrefab;
	[SerializeField] float inflationMultiplier = 15;

	void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(this);
	}

	public DamageNumberCanvas SpawnDamageNumbers(int damageNumber, Vector2 worldPosition)
	{
		DamageNumberCanvas damageNum = Instantiate(damageNumberPrefab, worldPosition, Quaternion.identity, transform);
		damageNum.SetDamageNumberText(Mathf.FloorToInt(damageNumber * inflationMultiplier));
		return damageNum;
	}

	public DamageNumberCanvas SpawnDamageNumbers(int damageNumber, Vector2 worldPosition, Color textColor)
	{
		DamageNumberCanvas damageNum = SpawnDamageNumbers(damageNumber, worldPosition);
		damageNum.SetDamageNumberColour(textColor);

		return damageNum;
	}
}