using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTransition : ITransition
{
	private GameObject gameObject;
	private AnimationCurve curve = null;
	private Vector2Int positionChange = Vector2Int.zero;

	public void Apply(float time)
	{
		Vector3 changeVector = GameUtils.GridToSpace(positionChange);
		if (curve != null)
		{
			time = curve.Evaluate(time);
		}

		gameObject.transform.localPosition += changeVector * time;
	}
}
