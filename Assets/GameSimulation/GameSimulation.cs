using System;
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
				if (TryPush(currentPiston.position, currentPiston.direction, currentState, nextState))
				{
					nextState.Pistons[i] = new PistonState(currentPiston)
					{
						order = PistonState.PistonOrder.None,
						isExtended = true
					};
					currentState.AddEvent(new PistonEvent(currentPiston.id, PistonEventType.Extend));
				}
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

	private static bool TryPush(Vector2Int position, GameUtils.Direction direction, GameState currentState, GameState nextState)
	{
		Vector2Int directionVector = GameUtils.DirectionVectors[(int)direction];
		Vector2Int pushIntoPosition = position + directionVector;
		GameState.SquareContent squareContent = currentState.GetSquareContent(pushIntoPosition);

		if (squareContent.pistonID != -1)
			return false;

		if (squareContent.crateID != -1)
		{
			if (TryPush(pushIntoPosition, direction, currentState, nextState))
			{
				CrateState newCrateState = new CrateState()
				{ 
					id = squareContent.crateID,
					position = pushIntoPosition + directionVector,
				};
				nextState.TrySetCrateState(newCrateState);
				currentState.AddEvent(new CrateMovedEvent(squareContent.crateID, CrateMovedEventType.Pushed, directionVector));
			}
			else return false;
		}

		return true;
	}
}
