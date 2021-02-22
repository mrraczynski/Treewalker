using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogController : MonoBehaviour
{

    public Canvas canv;
    public int deactivateTime;

    void Start ()
    {
        canv.gameObject.SetActive(false);
    }

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F) && canv.gameObject.activeSelf == false && gameObject.GetComponent<ObjectInteraction>().IsInteraction())
        {
            canv.gameObject.SetActive(true);
            StartCoroutine(DeactivateCanvas());
        }
    }


    IEnumerator DeactivateCanvas()
    {
        yield return new WaitForSeconds(deactivateTime);
        if (canv.gameObject.activeSelf == true)
        {
            canv.gameObject.SetActive(false);
        }
    }
}
