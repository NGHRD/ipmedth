using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordijn_Dicht : MonoBehaviour
{
    public GameObject gordijnLinks;
    public GameObject gordijnRechts;

    private Vector3 newscale;


    private void OnTriggerEnter(Collider other)
    {
        newscale = new Vector3(105, 100, 100);
        gordijnLinks.transform.localScale = newscale;
        gordijnRechts.transform.localScale = newscale;
    }


}
