using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField]
    private float MAX_VELOCITY_MAG;
    [SerializeField]
    private float MAX_ACCELERATION_MAG;

    private float currVelocity;
    private Vector2 currAcceleration;

    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent <Rigidbody2D>();
    }
    private void MovePlayer(Vector2 forceVec)
    {
        rb.AddForce(forceVec);
        currVelocity = rb.velocity.magnitude;

        if(currVelocity > MAX_VELOCITY_MAG)
        {
            rb.velocity = MAX_VELOCITY_MAG * gameObject.transform.forward;
        }
    }

    private void RotatePlayer(float rotation)
    {
        player.
    }
}
