using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject cratePrefab;

    [SerializeField]
    private GameObject pistonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameState state = new GameState();
        state.AddCrate(Vector2Int.right);
        state.AddCrate(Vector2Int.up);
        state.AddPiston(new Vector2Int(3, 0), GameUtils.Direction.East);
        state.AddPiston(new Vector2Int(1, 2), GameUtils.Direction.North);

        InstantiateGameObects(state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateGameObects(GameState state)
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
            GameObject crateObject = Instantiate(pistonPrefab, pos, Quaternion.Euler(0.0f, rotation, 0.0f), transform);
            // var crateView = crateObject.GetComponent<CrateView>();
            // crateView.CrateID = piston.id;
        }
    }
}
