using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject hourHand;
    [SerializeField] private GameObject minuteHand;
    [SerializeField] private GameObject secondHand;

    void Start()
    {
        System.DateTime theTime = System.DateTime.Now;
        int currMinute = theTime.Minute;
        int currHour = theTime.Hour;
        int currSecond = theTime.Second;
        hourHand.transform.Rotate(Vector3.right * ((30 * currHour) + (currMinute * 1 / 2)));
        minuteHand.transform.Rotate(Vector3.right * 6 * currMinute);
        secondHand.transform.Rotate(Vector3.right * currSecond);

    }
    void Update()
    {
        secondHand.transform.Rotate(6 * Vector3.right * Time.deltaTime);
        hourHand.transform.Rotate(1/120 * Vector3.right * Time.deltaTime);
        minuteHand.transform.Rotate(1 / 10 * Vector3.right * Time.deltaTime);
    }
}
