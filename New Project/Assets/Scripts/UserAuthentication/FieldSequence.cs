using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FieldSequence : MonoBehaviour
{
    private EventSystem system;

    public void fieldOrder()
    {
        system = EventSystem.current;
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
    }
}
