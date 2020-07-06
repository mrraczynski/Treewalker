using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointing : MonoBehaviour
{

    public bool trigger = false;
    public Collider2D characterCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        characterCollider = collision;
    }

}
