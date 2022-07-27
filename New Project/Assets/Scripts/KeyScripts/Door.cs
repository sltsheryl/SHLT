using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private FPS_UI fpsUi;
    
    private Outline outline;
    [SerializeField] private float fadeInDelay;
    [SerializeField] private CanvasGroup winScreen;
    [SerializeField] private CanvasManager canvasManager;

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
        if (inventory.Check() is KeyPuzzle)
        {
            inventory.Use();
            canvasManager.AddToCanvasStack(winScreen);
            StartCoroutine(FadeWinScreen());
        } else
        {
            fpsUi.SetMessageText("You need a key to do that...");
        }
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    
    IEnumerator FadeWinScreen()
    {
        for (float f = 0; f <= fadeInDelay; f += Time.deltaTime)
        {
            winScreen.alpha = Mathf.Lerp(0f, 1f, f / 2);
            yield return null;
        }
        winScreen.alpha = 1;
    }

}
