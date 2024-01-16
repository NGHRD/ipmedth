using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioOranje : MonoBehaviour
{
    public Transform radioLocation;
    public Radio_oranje radio;
    public GameObject radioGameObject;

    private bool isOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("radio") && !isOn) {


            radioGameObject.gameObject.transform.position = radioLocation.position;
            radioGameObject.gameObject.transform.rotation = radioLocation.rotation;
            isOn = true;
            radio.playRadioOranje();
            Debug.Log("werkt");
        }
    }
        
}