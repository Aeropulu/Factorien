using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeline
{
	private List<GameState> gameStates = null;

	public void SetStartState(GameState state)
	{
		if (gameStates == null)
			gameStates = new List<GameState>(8);
		else
			gameStates.Clear();

		gameStates.Add(state);
	}

	public GameState GetState(int index)
	{
		if (gameStates == null)
			return null;

		if (index < 0)
			index = 0;

		if (index >= gameStates.Count)
			index = gameStates.Count - 1;

		return gameStates[index];
	}

	public void ComputeStates(int startState, int endState)
	{
		if (endState <= startState || gameStates == null || gameStates.Count < startState || gameStates[startState] == null)
		{
			Debug.LogWarning("Tried to ComputeStates with inconsistant parameters.");
			return;
		}

		for (int nState = startState; nState < endState; nState++)
		{
			GameState currentState = gameStates[nState];
			GameState newState = GameSimulation.ComputeNextState(currentState);
			if (gameStates.Count <= nState + 1)
			{
				gameStates.Add(newState);
			}
			else
			{
				gameStates[nState + 1] = newState;
			}
		}
	}
}
