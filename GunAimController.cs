using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimController : MonoBehaviour
{
    Transform player;

    [Tooltip("The amount of smoothing to apply when moving the gun around the player")]
    public float moveSmoothTime = 1.0f;

    [Tooltip("The distance between the player and the gun (not exactly because of the smoothing)")]
    public float gunDistance = 1.0f;

    private Vector3 gunVelocity = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Moving
        Vector3 gunVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        gunVector = Vector3.Scale(gunVector, Vector3.one - Vector3.forward);

        Vector3 currentPos = Vector3.SmoothDamp(transform.position, player.position + (gunVector.normalized * gunDistance), ref gunVelocity, moveSmoothTime);

        transform.position = currentPos;

        //Rotate
        transform.right = gunVector;

        //Flip?
        float xAngle = transform.rotation.eulerAngles.z;

        if(xAngle > 90 && xAngle < 270)
        {
            transform.localScale = new Vector3(1,-1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
    }
}
