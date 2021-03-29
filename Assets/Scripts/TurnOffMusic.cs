using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffMusic : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !gameObject.GetComponent<AudioSource>().mute)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        else if(Input.GetKeyDown(KeyCode.M) && gameObject.GetComponent<AudioSource>().mute)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
    }
}
