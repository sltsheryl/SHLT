using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Takeable : Interactable
{
    [SerializeField] private Sprite takeableSprite;
    public virtual Sprite GetSprite()
    {
        return takeableSprite;
    }

    public abstract void OnTake();
}
