using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeline
{
	private List<GameState> gameStates;

	public void ComputeStates(int startState, int endState)
	{
		if (endState <= startState || gameStates == null || gameStates.Count <= startState || gameStates[startState] == null)
		{
			Debug.LogWarning("Tried to ComputeStates with inconsistant parameters.");
			return;
		}

		for (int nState = startState; nState <= endState; nState++)
		{
			GameState newState = gameStates[nState].ComputeNextState();
			if (gameStates.Count < nState)
			{
				gameStates.Add(newState);
			}
			else
			{
				gameStates[nState] = newState;
			}
		}
	}
}
