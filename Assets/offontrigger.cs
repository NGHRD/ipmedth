using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offontrigger : MonoBehaviour
{

    public GameObject LightSource;
    public GameObject LightSource2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (LightSource.activeSelf)
        LightSource.SetActive(!LightSource.activeInHierarchy);
        LightSource2.SetActive(!LightSource2.activeInHierarchy);
    }
}
