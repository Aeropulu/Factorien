using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils
{
	const float X_SCALE = 1.0f;
	const float Y_SCALE = 1.0f;
	public static Vector3 GridToSpace(Vector2Int gridVector)
	{
		float x = gridVector.x * X_SCALE;
		float y = gridVector.y * Y_SCALE;

		return new Vector3(x, 0.0f, y);
	}

	public enum Direction : int
	{
		East, South, West, North
	}

	public static readonly Vector2Int[] DirectionVectors = { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up };
}
