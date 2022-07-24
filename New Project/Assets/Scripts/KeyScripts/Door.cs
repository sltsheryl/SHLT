using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Inventory inventory;
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
        Debug.Log("Interact with Door");
        if (inventory.Use() is KeyPuzzle)
        {
            canvasManager.AddToCanvasStack(winScreen);
            StartCoroutine(FadeWinScreen());
            Debug.Log("You win!");
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
