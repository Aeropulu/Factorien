using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    void Apply(float time);
}
