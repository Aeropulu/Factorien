using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulation
{
    public static GameState ComputeNextState(GameState currentState)
	{
		GameState newState = currentState.Clone();
		RunPistons(currentState, newState);
		return newState;
	}

	private static void RunPistons(GameState currentState, GameState nextState)
	{
		int pistonCount = currentState.Pistons.Count;
		for (int i = 0; i < pistonCount; i++)
		{
			PistonState currentPiston = currentState.Pistons[i];
			if (currentPiston.order == PistonState.PistonOrder.Extend && currentPiston.isExtended == false)
			{
				// if can extend
				AnimationTransition transition = new AnimationTransition();
				nextState.Pistons[i] = new PistonState(currentPiston)
				{
					order = PistonState.PistonOrder.None,
					isExtended = true
				};
				currentState.AddEvent(new PistonEvent(currentPiston.id, PistonEventType.Extend));
			}

			if (currentPiston.order == PistonState.PistonOrder.Retract && currentPiston.isExtended)
			{
				nextState.Pistons[i] = new PistonState(currentPiston)
				{
					order = PistonState.PistonOrder.None,
					isExtended = false
				};
				currentState.AddEvent(new PistonEvent(currentPiston.id, PistonEventType.Retract));
			}
		}
	}
}
