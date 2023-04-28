using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTriggeredObject : MonoBehaviour
{
    public float speed;
   
   void OnTriggerStay(Collider col)
   {
    col.transform.position += transform.forward * speed * Time.deltaTime;
   }
}
