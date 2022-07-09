using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button quitApp;
    [SerializeField] private Button startGame;
    [SerializeField] private Button logOut;


    private void Start()
    {
        logOut.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        quitApp.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        startGame.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Transition"));
    }
}
