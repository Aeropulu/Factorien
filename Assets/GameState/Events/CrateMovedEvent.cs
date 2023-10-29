using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMovedEvent : IGameEvent
{
	public readonly int CrateID;
	public readonly CrateMovedEventType MovementType;
	public readonly Vector2Int MovementVector;

	public CrateMovedEvent(int ID, CrateMovedEventType type, Vector2Int vector)
	{
		CrateID = ID;
		MovementType = type;
		MovementVector = vector;
	}
}

public enum CrateMovedEventType
{
	None,
	Pushed,
	Conveyed
}