using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    Renderer playerRenderer;
    EnemyAI enemyScript;
    PlayerMovementUpdated playerScript;

    public Material invisibleMaterial;
    public Material normalMaterial;



    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GameObject.Find("Robot_low").GetComponent<MeshRenderer>();
        enemyScript = GameObject.Find("Enemy").GetComponent<EnemyAI>();

        playerScript = GameObject.Find("Updated 3rd Person Player").GetComponent<PlayerMovementUpdated>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            //InvisibleAbility();

            StartCoroutine(Invis(4f));
        }

    }

    /*
    public void InvisibleAbility()
    {
        playerRenderer.material = invisibleMaterial;
        enemyScript.playerInvisible = true;
    }

    public void StopInvisibility()
    {
        playerRenderer.material = normalMaterial;
        enemyScript.playerInvisible = false;
    }
    */

    IEnumerator Invis(float duration)
    {

        playerRenderer.material = invisibleMaterial;
        enemyScript.playerInvisible = true;
        playerScript.PlayInvisibility();

        yield return new WaitForSeconds(duration);

        playerRenderer.material = normalMaterial;
        enemyScript.playerInvisible = false;
    }
}
