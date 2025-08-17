using System.Collections.Generic;
using UnityEngine;

static public class DirectionProperties
{
	public enum DirectionProperty
	{
		ObjectToMouseDirection,
		InheritFromSelfHitbox,
		Vector2Right,
		Random,
		NearestEnemy,
		NoDirection
	}

	static Vector2 GetDirectionToClosestEnemy(Transform transform, List<EnemyHP> dontIncludeEnemies = null)
	{
		EnemyMovement[] enemies = Object.FindObjectsOfType<EnemyMovement>();
		Transform closest = null;
		float minDistance = Mathf.Infinity;

		foreach (EnemyMovement enemy in enemies)
			if (dontIncludeEnemies == null || !dontIncludeEnemies.Contains(enemy.enemyHP))
			{
				float distance = Vector2.Distance(transform.position, enemy.transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					closest = enemy.transform;
				}
			}

		if (closest != null)
		{
			//Debug.DrawLine(closest.position, transform.position);
			Vector2 dir = (closest.position - transform.position).normalized;
			return dir;
		}

		return Vector2.zero; // no enemy found
	}

	public static Vector2 GetDirectionFromProperty(DirectionProperty directionType, Transform source, GameObject self, List<EnemyHP> hitboxLockout = null)
	{
		Vector2 dir = Vector2.zero;
		switch (directionType)
		{
			case DirectionProperty.ObjectToMouseDirection:
				dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - source.position).normalized;
				break;
			case DirectionProperty.InheritFromSelfHitbox:
				if (self == null) break;

				// attempt to inherit direction from gameobject
				Hitbox hitbox = self.GetComponent<Hitbox>();
				if (hitbox != null)
					dir = hitbox.GetDirection();
				break;
			case DirectionProperty.Vector2Right:
				dir = Vector2.right;
				break;
			case DirectionProperty.Random:
				dir = Random.insideUnitCircle.normalized;
				break;
			case DirectionProperty.NearestEnemy:
				dir = GetDirectionToClosestEnemy(source, hitboxLockout);
				if (dir == Vector2.zero) goto case DirectionProperty.Random;
				break;
			default:
			case DirectionProperty.NoDirection:
				break;
		}
		return dir;
	}
}
