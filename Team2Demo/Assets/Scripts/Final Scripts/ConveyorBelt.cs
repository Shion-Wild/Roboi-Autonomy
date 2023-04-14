using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed, conveyorSpeed;
    public Vector3 direction;
    public List<GameObject> onBelt;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void Update()
    {
        //GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i=0; i <= onBelt.Count -1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
        }
    }

    //When something colides with belt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    //When something leaves the belt
    private void OnCollisoinExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
