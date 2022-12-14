using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject go;

    void Update()
    {
        if (go.transform.localScale.x >= 7.5326)
        {
            SceneManager.LoadScene("RoomOne");
            FPSController.CanMove = true;
            FPSController.CanLook = true;
        }
    }
}
