using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PistonState
{
    public int id;
    public Vector2Int position;
    public GameUtils.Direction direction;
    public bool isExtended;
    public PistonOrder order;
    public enum PistonOrder
	{
        None,
        Extend,
        Retract
	}
}
