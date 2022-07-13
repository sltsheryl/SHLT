using UnityEngine;
using System.Collections;

public class Words : MonoBehaviour
{
    [SerializeField] private Switch lightSwitch;
    private LightObserver lightObserver;

    private void Start()
    {
        LightSubject lightSubject = lightSwitch.GetLightSubject();
        lightObserver = new LightObserver(this, lightSubject);
    }

    public void UpdateState(bool state)
    {
        bool currentLighted = state;
        if (currentLighted)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Unshow words");

        }
        else
        {
            this.gameObject.SetActive(true);
            Debug.Log("Show words");
        }
    }
}
