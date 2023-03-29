using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private SpringJoint joint;
    private float maxDistance = 100f;


    void Awake() 
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.X))
        {
            StartGrapple();
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
         DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin: camera.position, direction: camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance( a: player.position, b: grapplePoint);

            //The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Change these to fit our needs
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint)
        {
            return;
        }
        lr.SetPosition( index: 0, gunTip.position);
        lr.SetPosition( index: 1, grapplePoint);

    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

    }
}
