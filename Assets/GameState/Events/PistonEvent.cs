using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonEvent : IGameEvent
{
	public readonly int pistonID;
	public readonly bool hasExtended;
	public readonly bool hasRetracted;

	public PistonEvent(int ID, PistonEventType type)
	{
		pistonID = ID;
		hasExtended = type == PistonEventType.Extend;
		hasRetracted = type == PistonEventType.Retract;
	}
}

public enum PistonEventType
{
	None, Extend, Retract
}