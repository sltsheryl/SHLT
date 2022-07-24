using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ITakeable takeable;

    public void Take(ITakeable takeable)
    {
        this.takeable = takeable;
        Debug.Log(this.takeable);
    }

    public ITakeable Use()
    {
        return takeable;
    }
}
