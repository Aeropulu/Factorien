using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameState
{
	private List<CrateState> crates = new List<CrateState>(8);
	private List<PistonState> pistons = new List<PistonState>(8);

	private List<IGameEvent> events = new List<IGameEvent>(8);

	public List<CrateState> Crates { get => crates; }
	public List<PistonState> Pistons { get => pistons; }
	public List<IGameEvent> Events { get => events; }
	public void AddCrate(Vector2Int position)
	{
		CrateState crate = new CrateState()
		{
			position = position,
			id = GetNewCrateID()
		};

		crates.Add(crate);
	}

	private int GetNewCrateID()
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

	public bool TryGetCrateState(int id, out CrateState outCrate)
	{
		foreach (var crate in crates)
		{
			if (crate.id == id)
			{
				outCrate = crate;
				return true;
			}
		}

		outCrate = new CrateState();
		return false;
	}

	public void AddPiston(Vector2Int position, GameUtils.Direction direction)
	{
		PistonState piston = new PistonState()
		{
			position = position,
			id = GetNewPistonID(),
			direction = direction
		};

		pistons.Add(piston);
	}

	private int GetNewPistonID()
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

	public bool TryGetPistonState(int id, out PistonState outPiston)
	{
		foreach (var piston in pistons)
		{
			if (piston.id == id)
			{
				outPiston = piston;
				return true;
			}
		}

		outPiston = new PistonState();
		return false;
	}

	public void SetPistonOrder(int id, PistonState.PistonOrder order)
	{
		for (int i = 0; i < pistons.Count; i++)
		{
			var piston = pistons[i];
			if (piston.id == id)
			{
				pistons[i] = new PistonState(piston)
				{
					order = order
				};
			}
		}
	}

	public void AddEvent(IGameEvent gameEvent)
	{
		if (events == null)
			events = new List<IGameEvent>(8);

		events.Add(gameEvent);
	}

	public GameState Clone()
	{
		GameState newState = new GameState();
		newState.crates = new List<CrateState>(crates);
		newState.pistons = new List<PistonState>(pistons);

		return newState;
	}
}
