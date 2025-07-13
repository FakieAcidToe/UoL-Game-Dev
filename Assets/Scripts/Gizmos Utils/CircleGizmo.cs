using UnityEngine;

public static class CircleGizmo
{
	public static void DrawCircle(Vector3 center, float radius = 1f, int segments = 32)
	{
		float angleStep = 360f / segments;
		Vector3 prevPoint = center + new Vector3(Mathf.Cos(0f * Mathf.Deg2Rad), Mathf.Sin(0f * Mathf.Deg2Rad)) * radius;

		for (int i = 1; i <= segments; ++i)
		{
			float angle = i * angleStep;
			Vector3 newPoint = center + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
			Gizmos.DrawLine(prevPoint, newPoint);
			prevPoint = newPoint;
		}
	}
}