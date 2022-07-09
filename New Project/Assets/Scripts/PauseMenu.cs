using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeGame;
    [SerializeField] private Button quitApp;
    [SerializeField] private Button exitGame;
    [SerializeField] private CanvasManager canvasManager;


    private void Start()
    {
        quitApp.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        exitGame.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        resumeGame.GetComponent<Button>().onClick.AddListener(() =>
        {
            canvasManager.PopFromCanvasStack();
        });
    }
}
