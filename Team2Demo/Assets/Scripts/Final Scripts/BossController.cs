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
    public GameObject CutCam;
    public GameObject PlayerCam;

    [SerializeField] GameObject winMenu;

    float waitTime = 3f;

    void Start()
    {
        consolesDestroyed = 0;
        
    }

    void Update()
    {
        if (consolesDestroyed >= 3)
        {
            StartCoroutine(BossWinTransition());
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

    private IEnumerator BossWinTransition()
    {
        //Disable playerCam and enable cutCam to display boss explosion
        PlayerCam.SetActive(false);
        CutCam.SetActive(true);

        Instantiate(BoomFXOne, BoomFXOneLocation.transform.position, Quaternion.identity);
        Instantiate(BoomFXTwo, BoomFXTwoLocation.transform.position, Quaternion.identity);

        Destroy(bossAI);

        yield return new WaitForSeconds(waitTime);

        //Destroy(BoomFXOne.gameObject);
        //Destroy(BoomFXTwo.gameObject);

        winMenu.SetActive(true);
    }
}
