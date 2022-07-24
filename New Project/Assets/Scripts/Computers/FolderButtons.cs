using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderButtons : MonoBehaviour
{
    [SerializeField] private FileManager fileManager;
    [SerializeField] private GameObject nextFolder;
    public void addAndSetNextFolder()
    {
        fileManager.addNextFolder(nextFolder);
        fileManager.addLetter(gameObject.name);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
