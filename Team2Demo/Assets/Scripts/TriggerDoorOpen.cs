using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerDoorOpen : MonoBehaviour
{
    //public Animation openDoor;
    public Animation empOpenDoor;

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EMP"))
        {
            Invoke("OpenEMPDoor", 2.5f);

        }
    }

    void OpenEMPDoor()
    {
        empOpenDoor.Play();
    }
   

}
