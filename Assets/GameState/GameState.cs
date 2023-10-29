using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameState
{
	private List<CrateState> crates = null;
	private List<PistonState> pistons = null;
	private List<ITransition> transitions = null;

	public ReadOnlyCollection<CrateState> Crates { get => crates.AsReadOnly(); }
	public ReadOnlyCollection<PistonState> Pistons { get => pistons.AsReadOnly(); }
	public void AddCrate(Vector2Int position)
	{
		if (crates == null)
			crates = new List<CrateState>(8);

		CrateState crate = new CrateState();
		crate.position = position;
		crate.id = GetCrateID();

		crates.Add(crate);
	}

	private int GetCrateID()
	{
		if (crates.Count == 0)
		{
			return 0;
		}
		else
		{
			return crates[crates.Count - 1].id + 1;
		}
	}

	public void AddPiston(Vector2Int position, GameUtils.Direction direction)
	{
		if (pistons == null)
			pistons = new List<PistonState>(8);

		PistonState piston = new PistonState();
		piston.position = position;
		piston.id = GetPistonID();
		piston.direction = direction;

		pistons.Add(piston);
	}

	private int GetPistonID()
	{
		if (pistons.Count == 0)
		{
			return 0;
		}
		else
		{
			return pistons[pistons.Count - 1].id + 1;
		}
	}

	public void ApplyTransitions(float time)
	{
		foreach(ITransition transition in transitions)
		{
			transition.Apply(time);
		}
	}

	public GameState Clone()
	{
		GameState newState = new GameState();
		newState.crates = new List<CrateState>(crates);

		return newState;
	}

	public GameState ComputeNextState()
	{
		GameState newState = Clone();
		return newState;
	}
}
