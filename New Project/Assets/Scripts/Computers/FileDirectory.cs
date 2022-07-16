using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FileDirectory : MonoBehaviour
{
    private List<CanvasGroup> directoryList;
    private CanvasGroup currentDirectory;

    private int curr;

    [SerializeField] private TextMeshProUGUI directoryName;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button Desktop;

    [SerializeField] private CanvasManager canvasManager;

    [SerializeField] private CanvasGroup Folders1;
    [SerializeField] private CanvasGroup Mouse2;
    [SerializeField] private CanvasGroup Cat2;
    [SerializeField] private CanvasGroup Python2;
    [SerializeField] private CanvasGroup Asia3;
    [SerializeField] private CanvasGroup Africa3;
    [SerializeField] private CanvasGroup NorthAmerica3;
    [SerializeField] private CanvasGroup SouthAmerica3;
    [SerializeField] private CanvasGroup Australia3;
    [SerializeField] private CanvasGroup Antartica3;
    [SerializeField] private CanvasGroup Europe3;
    [SerializeField] private CanvasGroup CS21093;
    [SerializeField] private CanvasGroup SOC4;
    [SerializeField] private CanvasGroup Genshin4;
    [SerializeField] private CanvasGroup MANGA4;


    private void Start()
    {
        canvasManager.AddToCanvasStack(Folders1);
        directoryList = new List<CanvasGroup>();
        directoryList.Add(Folders1);
        currentDirectory = Folders1;
        directoryName.text = "Desktop";
        curr = 0;

        Desktop.onClick.AddListener((() =>
        {
            goBackHome();
        }));

        backButton.onClick.AddListener(() =>
        {
            if (curr > 0)
            {
                curr -= 1;
                canvasManager.removeCanvas(currentDirectory);
                canvasManager.AddToCanvasStack(directoryList[curr]);
                currentDirectory = directoryList[curr];
            } 
        });

        nextButton.onClick.AddListener(() =>
        {
            if (curr < directoryList.Count - 1)
            {
                curr += 1;
                canvasManager.removeCanvas(currentDirectory);
                canvasManager.AddToCanvasStack(directoryList[curr]);
                currentDirectory = directoryList[curr];
            }
        });
    }

    private void goBackHome()
    {
        currentDirectory = Folders1;
        directoryList.Add(currentDirectory);
        directoryName.text = "Desktop";
        curr += 1;
    }

    public void clickFolder(GameObject go)
    {
        CanvasGroup clickedDirectory = getDirectory(go);
        if (clickedDirectory != null)
        {
            canvasManager.removeCanvas(currentDirectory);
            currentDirectory = clickedDirectory;
            directoryList.Add(currentDirectory);
            curr += 1;
            canvasManager.AddToCanvasStack(currentDirectory);
        }
    }

    public void directoryLabel (string name)
    {
        directoryName.text = name;
    }

    private void Update()
    {
        directoryName.text = currentDirectory.gameObject.name;
    }

    private CanvasGroup getDirectory(GameObject x)
    {
        string[] name = { "Mouse", "Cat", "Python", "Asia", "Africa", "North America", "South America", "Antartica", "Europe", "Australia", "NUS", "Genshin", "MANGA", "CS2109" };
        CanvasGroup[] lst = { Mouse2, Cat2, Python2, Asia3, Africa3, NorthAmerica3, SouthAmerica3, Antartica3, Europe3, Australia3, SOC4, Genshin4, MANGA4, CS21093 };
        for (int i = 0; i < name.Length; i++)
        {
            if (x.name == name[i])
            {
                return lst[i];
            }
        } 
        
            return null;
        
    }
}
