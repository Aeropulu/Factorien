using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateView : MonoBehaviour
{
    [SerializeField]
    private int crateID = -1;

    public int CrateID
    {
        get => crateID;
        set
        {
            if (crateID == -1)
			{
                crateID = value;
			}
        }
    }

}
