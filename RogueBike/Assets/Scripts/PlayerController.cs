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

    //Card to turn
    [SerializeField]
    private List<GameObject> cards;
    private Dictionary<string, TurnCard> turnDict = new Dictionary<string, TurnCard>();

    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        for(int i = 0; i < cards.Count; i++)
        {
            Debug.Log(cards[i].name);
            turnDict[cards[i].name] = cards[i].GetComponent<TurnCard>();
        }
    }

    public void CheckForInput() {
        //Debug.Log(rb.velocity);
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

        //play card. Checking on up to make it only happen once
        if (Input.GetKeyUp(KeyCode.M))
        {
            player.transform.Rotate(0, 0, turnDict["RightTurn"].Angle);
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            player.transform.Rotate(0, 0, turnDict["LeftTurn"].Angle);
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

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log(collider.gameObject.name);
        Debug.Log(collider.GetContact(0));
        Vector3 normal = collider.GetContact(0).normal;
        Debug.Log(Vector3.Angle(normal, rb.velocity));
        Debug.Log(collider.GetContact(0).relativeVelocity);
        rb.drag = 8;
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        rb.angularVelocity = 0;
        rb.drag = 2;
    }
}
