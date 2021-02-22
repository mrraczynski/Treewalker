using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    public float textWait = 5;
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextWait());
    }

    IEnumerator TextWait()
    {
        yield return new WaitForSeconds(textWait);
        Destroy(gameObject);
        obj.GetComponent<ObjectInteraction>().interactionIcon.SetActive(true);
    }
}
