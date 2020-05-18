using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private Vector3 restrictedOffset;
    public void Start()
    {
        restrictedOffset = transform.position - target.position;
    }
    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameScript.GamePhase == 2)
        {
            if (target == null)
            {
                Debug.LogWarning("Missing target ref !", this);

                return;
            }

            // compute position
            if (offsetPositionSpace == Space.Self)
            {
                transform.position = target.TransformPoint(offsetPosition);
            }
            else
            {
                transform.position = target.position + offsetPosition;
            }

            // compute rotation
            if (lookAt)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = target.rotation;
            }
        }
        else
            RestrictedMovement();
    }

    private void RestrictedMovement()
    {
        transform.position = target.position + restrictedOffset;
    }

}
