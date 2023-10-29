using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionParameters : MonoBehaviour
{
    public static TransitionParameters instance = null;

    [SerializeField]
    private AnimationCurve pistonExtendCurve = null;

    [SerializeField]
    private AnimationClip pistonExtendClip = null;

    public static AnimationCurve PistonExtendCurve { get => instance == null ? null : instance.pistonExtendCurve; }
    public static AnimationClip PistonExtendClip { get => instance == null ? null : instance.pistonExtendClip; }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

}
