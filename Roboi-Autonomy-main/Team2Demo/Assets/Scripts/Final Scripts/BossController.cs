using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static int consolesDestroyed;

    void Start()
    {
        consolesDestroyed = 0;
        
    }

    void Update()
    {
        if (consolesDestroyed >= 3)
        {
            // Call Win Scene
        }
        
    }

    public void DisabledConsoleOne()
    {
        consolesDestroyed++;
        Debug.Log("Console One Destroyed!");
    }
}
