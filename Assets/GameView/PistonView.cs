using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonView : MonoBehaviour
{
    [SerializeField]
    private int pistonID = -1;

    public int PistonID
    {
        get => pistonID;
        set
        {
            if (pistonID == -1)
            {
                pistonID = value;
            }
        }
    }

    public void UpdateView(GameState state, float time = 0.0f)
	{
        if (state.TryGetPistonState(pistonID, out PistonState piston))
		{
            transform.localPosition = GameUtils.GridToSpace(piston.position);
            float rotation = (int)piston.direction * 90.0f;
            transform.localRotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            float animTime = piston.isExtended ? 1.0f : 0.0f;
            TransitionParameters.PistonExtendClip.SampleAnimation(gameObject, animTime);
		}
	}
}
