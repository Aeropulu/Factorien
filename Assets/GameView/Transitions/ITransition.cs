using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    void Apply(float time);

    public static ITransition CreateFromEvent(IGameEvent gameEvent)
	{
		if (gameEvent is PistonEvent)
		{
			return new AnimationTransition();
		}

		else return null;
	}
}
