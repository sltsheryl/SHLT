using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class SafePuzzle : MonoBehaviour
{

    public string correctPassword = "123";
    private string input;
    public GameObject knob;
    //public AudioSource audioData;
    float rotationAmount = 0.5f;
    float delaySpeed = 0.05f;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform)
                {
                    print(hit.transform.gameObject.name);
                    string target = hit.transform.gameObject.name;
                    //if enter
                    if (target == "Enter")
                    {
                        if (input.Equals(correctPassword))
                        {
                            StartCoroutine(openSafe());
                        }
                        else
                        {
                            wrongPassword();
                        }
                    }

                    //if reset
                    if (target == "Reset")
                    {
                        input = "";
                    }

                    //if number
                    bool present = false;
                    for (int i = 0; i < nums.Length; i++)
                    {
                        if (nums[i] == target)
                        {
                            present = present || true;
                        }
                    }
                    if (present)
                    {
                        input += target;
                    }

                    if (target == "Back")
                    {
                        goBack();
                    }
                }
            }
        }
    }

    //void openSafe()
    //{
    //    Vector3 direction = new Vector3(0, 90, 0);
    //    Quaternion targetRotation = Quaternion.Euler(direction);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 20f);
    //    //float speed = 10F;
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * speed);
    //    //knob.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 270), Time.deltaTime * speed);
    //}
    IEnumerator openSafe()
    {
        float count = 0;
        while (count <= 90)
        {
            gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0));
            count += rotationAmount;
            yield return new WaitForSeconds(delaySpeed);
        }
    }
    void wrongPassword()
    {
        Debug.Log("Wrong pin!");
        input = "";
    }

    void goBack()
    {
        Debug.Log("Lol");
        SceneManager.LoadScene("RoomOne");
    }
}
