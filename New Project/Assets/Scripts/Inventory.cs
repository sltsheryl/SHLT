using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private FPS_UI fpsUi;
    private Takeable takeable;

    public void Take(Takeable takeable)
    {
        this.takeable = takeable;
        fpsUi.SetImage(takeable.GetSprite());
    }

    public Takeable Check()
    {
        return takeable;
    }

    public Takeable Use()
    {
        Takeable temp = takeable;
        takeable = null;
        fpsUi.SetImage(null);
        return temp;
    }
}
