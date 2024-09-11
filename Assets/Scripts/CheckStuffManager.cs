using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStuffManager : MonoBehaviour
{
    public static CheckStuffManager INSTANCE;

    public bool flashlightOn;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
