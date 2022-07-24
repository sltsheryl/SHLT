using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FileManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI directoryName;
    private List<GameObject> lst;
    [SerializeField] private GameObject firstFG;
    private int current;

    private void Start()
    {
        current = 0;
        lst = new List<GameObject>();
        lst.Add(firstFG);
    }


    public void addNextFolder(GameObject folder)
    {
        lst.Add(folder);
        current += 1;
        folder.SetActive(true);
    }

    public void addLetter(string s)
    {
        directoryName.text += s + "/";
    }

    public void back()
    {  
        if (current > 0)
        {
            current -= 1;

            string currName = directoryName.text;
           
                directoryName.text = directoryName.text.Substring(0, currName.Length - 2);
           
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != current)
                {
                    lst[i].gameObject.SetActive(false);
                }
            }
            lst[current].gameObject.SetActive(true);
           

        }
    }
    
    public void backToHome()
    {
        lst[current].gameObject.SetActive(false);
        addLetter(":");
        addNextFolder(firstFG);
        
        
    }
 
    

}
