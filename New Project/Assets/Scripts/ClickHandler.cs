using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2.0f;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

<<<<<<< Updated upstream
        //    if (Physics.Raycast(ray, out hit, maxDistance))
        //    {
        //        if (hit.transform != null)
        //        {
        //            IPointerClickHandler clicked = hit.transform.GetComponent<IPointerClickHandler>();
        //            if (clicked != null)
        //            {
        //                Debug.Log(clicked);
        //            }
        //        }
        //    }
        //}
=======
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform != null)
                {
                    string target = hit.transform.gameObject.name;
                    PrintName(hit.transform.gameObject);

                    //if (target == "wallA" || target == "wallB" || target == "wallC")
                    //{
                    //    Debug.Log("Play");
                    //    if (distanceTraveled < 100000f)
                    //    {
                    //        Vector3 oldPosition = go.transform.position;
                    //        go.transform.Translate(0, 0, 10 * Time.deltaTime);
                    //        distanceTraveled += Vector3.Distance(oldPosition, go.transform.position);
                    //    }
                    //}

                }
            }
        }
>>>>>>> Stashed changes
    }

    private void PrintName(GameObject go)
    {
        Debug.Log(go.name);
    }
}
