using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = player.GetComponent<PlayerController>().GetHealth().ToString();
    }
}
