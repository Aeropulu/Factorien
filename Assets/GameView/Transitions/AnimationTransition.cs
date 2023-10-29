using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTransition : ITransition
{
	private GameObject gameObject;
	private AnimationCurve curve = null;
	private AnimationClip clip = null;

	public AnimationTransition(GameObject gameObject, AnimationCurve animationCurve, AnimationClip animationClip)
	{
		this.gameObject = gameObject;
		curve = animationCurve;
		clip = animationClip;
	}

	public void Apply(float time)
	{
		if (clip == null)
			return;

		if (curve != null)
			time = curve.Evaluate(time);

		clip.SampleAnimation(gameObject, time);
	}
}
