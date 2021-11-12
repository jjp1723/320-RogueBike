using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField]
    private float forwardForceMagnitude;
    [SerializeField]
    private float brakeForceMagnitude;
    [SerializeField]
    private float rotationalSpeed;

    [SerializeField]
    private float MAX_VELOCITY_MAG;
    [SerializeField]
    private float MAX_ACCELERATION_MAG;

    private float currVelocity;
    private Vector2 currAcceleration;

    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
    }

    public void CheckForInput() {
        Debug.Log(rb.velocity);
        if (Input.GetKey(KeyCode.A))
        {
            RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotatePlayer(-rotationalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(forwardForceMagnitude * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(brakeForceMagnitude * Time.deltaTime);
        }
        
    }

    private void MovePlayer(float force)
    {
        rb.AddForce(force * player.transform.up);
        currVelocity = rb.velocity.magnitude;

        if (currVelocity > MAX_VELOCITY_MAG)
        {
            rb.velocity = MAX_VELOCITY_MAG * player.transform.forward;
        }
    }

    private void RotatePlayer(float rotation)
    {
        player.transform.rotation = Quaternion.Euler(player.transform.forward * rotation + player.transform.eulerAngles);
    }
}
