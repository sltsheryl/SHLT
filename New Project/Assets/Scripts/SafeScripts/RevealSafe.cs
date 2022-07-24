using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealSafe : Interactable
{
    [SerializeField] private Safe safe;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Wall clicked!");
        safe.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }


}
