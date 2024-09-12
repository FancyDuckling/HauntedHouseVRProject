using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    //public AudioSource turnOn;
    //public AudioSource turnOff;

    public bool on;
    public bool off;


    void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }

    void Update()
    {
        if (off && OVRInput.Get(OVRInput.Button.One))
        {
            flashlight.SetActive(true);
            //turnOn.Play();
            //CheckStuffManager.INSTANCE.flashlightOn = true;
            off = false;
            on = true;
        }
        else if (on && OVRInput.Get(OVRInput.Button.One))
        {
            flashlight.SetActive(false) ;
            //turnOff.Play();
            //CheckStuffManager.INSTANCE.flashlightOn = false;
            off =true;
            on = false;
        }
    }
}
