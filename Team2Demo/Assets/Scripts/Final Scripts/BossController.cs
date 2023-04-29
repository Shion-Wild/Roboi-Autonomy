using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] BossAI bossAI;
    [SerializeField] GameObject BoomFXOne;
    [SerializeField] GameObject BoomFXTwo;
    [SerializeField] GameObject BoomFXOneLocation;
    [SerializeField] GameObject BoomFXTwoLocation;
    public static int consolesDestroyed;

    void Start()
    {
        consolesDestroyed = 0;
        
    }

    void Update()
    {
        if (consolesDestroyed >= 3)
        {
            // Destroy Boss AI controller to make him stop moving
            Destroy(bossAI);

            // Call BossDestroyed function to play Animation
            Instantiate(BoomFXOne, BoomFXOneLocation.transform.position, Quaternion.identity);
            Instantiate(BoomFXTwo, BoomFXTwoLocation.transform.position, Quaternion.identity);

            Destroy(BoomFXOne.gameObject);
            Destroy(BoomFXTwo.gameObject);



            // Call Win Scene
        }
        
    }

    public void DisabledConsoleOne()
    {
        consolesDestroyed++;
        Debug.Log("Console One Destroyed!");
    }

    public void DisabledConsoleTwo()
    {
        consolesDestroyed++;
        Debug.Log("Console Two Destroyed!");
    }

    public void DisabledConsoleThree()
    {
        consolesDestroyed++;
        Debug.Log("Console Three Destroyed!");
    }
}
