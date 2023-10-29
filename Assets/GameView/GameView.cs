using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject cratePrefab;

    [SerializeField]
    private GameObject pistonPrefab;

    private GameTimeline gameTimeline;

    private List<CrateView> crateViews = new List<CrateView>();
    private List<PistonView> pistonViews = new List<PistonView>();

    private List<ITransition> transitions = new List<ITransition>();
    // Start is called before the first frame update
    void Start()
    {
        gameTimeline = new GameTimeline();

        GameState state = new GameState();
        state.AddCrate(Vector2Int.right);
        state.AddCrate(Vector2Int.up);
        state.AddPiston(new Vector2Int(3, 0), GameUtils.Direction.East);
        state.AddPiston(new Vector2Int(1, 2), GameUtils.Direction.North);
        state.SetPistonOrder(0, PistonState.PistonOrder.Extend);

        gameTimeline.SetStartState(state);
        gameTimeline.ComputeStates(0, 16);

        InstantiateGameObjects(state);
        CreateTransitionsFromEvents(state);
    }

    public void UpdateView(float time)
	{
        int stateIndex = Mathf.FloorToInt(time);

        GameState state = gameTimeline.GetState(stateIndex);

        foreach (PistonView view in pistonViews)
		{
            view.UpdateView(state);
		}
	}

    private void InstantiateGameObjects(GameState state)
	{
        InstantiateCrates(state);
        InstantiatePistons(state);

    }

    private void InstantiateCrates(GameState state)
	{
        if (cratePrefab == null)
            return;

        var crates = state.Crates;
        if (crates == null)
            return;

        foreach (CrateState crate in crates)
        {
            Vector3 pos = GameUtils.GridToSpace(crate.position);
            GameObject crateObject = Instantiate(cratePrefab, pos, Quaternion.identity, transform);
            var crateView = crateObject.GetComponent<CrateView>();
            crateView.CrateID = crate.id;
            crateViews.Add(crateView);
        }
    }

    private void InstantiatePistons(GameState state)
	{
        if (pistonPrefab == null)
            return;

        var pistons = state.Pistons;
        if (pistons == null)
            return;

        foreach (PistonState piston in pistons)
        {
            Vector3 pos = GameUtils.GridToSpace(piston.position);
            float rotation = (int)piston.direction * 90.0f;
            GameObject pistonObject = Instantiate(pistonPrefab, pos, Quaternion.Euler(0.0f, rotation, 0.0f), transform);
            var pistonView = pistonObject.GetComponent<PistonView>();
            pistonView.PistonID = piston.id;
            pistonViews.Add(pistonView);
        }
    }

    private void CreateTransitionsFromEvents(GameState state)
	{
        transitions.Clear();
        foreach(IGameEvent gameEvent in state.Events)
		{
            ITransition transition = ITransition.CreateFromEvent(gameEvent);
            if (transition != null)
			{
                transitions.Add(transition);
			}
		}
	}
}
