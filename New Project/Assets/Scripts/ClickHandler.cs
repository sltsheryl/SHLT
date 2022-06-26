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
    }

    private void PrintName(GameObject go)
    {
        Debug.Log(go.name);
    }
}
