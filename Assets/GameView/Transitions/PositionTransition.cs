using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTransition : ITransition
{
	private GameObject gameObject;
	private AnimationCurve curve = null;
	private Vector3 positionChange = Vector3.zero;

	public PositionTransition(GameObject gameObject, AnimationCurve animationCurve, Vector3 vector)
	{
		this.gameObject = gameObject;
		curve = animationCurve;
		positionChange = vector;
	}

	public void Apply(float time)
	{
		if (curve != null)
		{
			time = curve.Evaluate(time);
		}

		gameObject.transform.localPosition += positionChange * time;
	}
}
