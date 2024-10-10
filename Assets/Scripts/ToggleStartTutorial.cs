using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleStartTutorial : MonoBehaviour
{
    public GameObject tutorialNote;
    
    // Start is called before the first frame update
    void Start()
    {
        tutorialNote.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            tutorialNote.SetActive(!tutorialNote.activeSelf);
        }

    }
}
