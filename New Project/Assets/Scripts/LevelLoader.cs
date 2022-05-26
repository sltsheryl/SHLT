using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Button submitButton;
    public Animator transition;
    public string sceneTwo;
    public float transitionTime = 1f;
    // Update is called once per frame
    private void Start()
    {
        submitButton.GetComponent<Button>().onClick.AddListener(() => LoadNextLevel());

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetSceneByName(sceneTwo).buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
