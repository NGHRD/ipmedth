using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordijn_Dicht : MonoBehaviour
{
    public GameObject gordijnLinks;
    public GameObject gordijnRechts;
    private bool isDicht = false;

    private Vector3 newscale;


    private void OnTriggerEnter(Collider other)
    {
        newscale = new Vector3(105, 100, 100);
        gordijnLinks.transform.localScale = newscale;
        gordijnRechts.transform.localScale = newscale;
        Interact();
    }

    private void Interact()
    {
        isDicht = true;
        CandleManager.Instance.CheckAllCandlesLit();
    }

    public bool IsDicht()
    {
        return isDicht;
    }

}
