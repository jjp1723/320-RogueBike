using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private GameObject frontTire;
    private GameObject handleBars;
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
    private float tireRotationFromBody = 0.0f;
    private bool tireRotatedLastFrame = false;

    private int lap = 1;
    public int Lap { get { return lap; } }

    private const int maxLap = 3;
    public int MaxLap { get { return maxLap; } }

    private int currCheckpoint = 1;
    private const int lastCheckpoint = 4;

    public float RotationalSpeed
    {
        get
        {
            return rotationalSpeed;
        }
    }

    public float ForwardForceMagnitude
    {
        get
        {
            return forwardForceMagnitude;
        }
    }

    //Card to turn
    //[SerializeField]
    //private List<GameObject> cards;
    //private Dictionary<string, TurnCard> turnDict = new Dictionary<string, TurnCard>();

    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        //for(int i = 0; i < cards.Count; i++)
        //{
        //    Debug.Log(cards[i].name);
        //    turnDict[cards[i].name] = cards[i].GetComponent<TurnCard>();
        //}

        frontTire = player.transform.Find("Tire Front").gameObject;
        handleBars = player.transform.Find("Handlebars").gameObject;
    }

    public void CheckForInput() {
        tireRotatedLastFrame = false;

        if (Input.GetKey(KeyCode.A))
        {
            RotatePlayer(rotationalSpeed * Time.deltaTime);
            tireRotatedLastFrame = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotatePlayer(-rotationalSpeed * Time.deltaTime);
            tireRotatedLastFrame = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(forwardForceMagnitude * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity != Vector2.zero)
            {
                MovePlayer(-brakeForceMagnitude * Time.deltaTime);
            }

            if (rb.velocity.magnitude < 2.0f)
            {
                rb.velocity = Vector2.zero;
            }
        }


        if (/*!tireRotatedLastFrame && */tireRotationFromBody != 0)
        {
            if (Mathf.Abs(tireRotationFromBody) <= 0)
            {
                tireRotationFromBody = 0;
                frontTire.transform.rotation = Quaternion.Euler(frontTire.transform.forward * (rotationalSpeed * Time.deltaTime) * -Mathf.Sign(tireRotationFromBody) + frontTire.transform.eulerAngles);
            }
            else
            {
                tireRotationFromBody += rotationalSpeed * Time.deltaTime * -Mathf.Sign(tireRotationFromBody);
                frontTire.transform.rotation = Quaternion.Euler(frontTire.transform.forward * (rotationalSpeed * Time.deltaTime) * -Mathf.Sign(tireRotationFromBody) + frontTire.transform.eulerAngles);
            }

            handleBars.transform.rotation = frontTire.transform.rotation;
        }

        ////can only play cards if paused
        //if (Time.timeScale < 1)
        //{
        //    //play card. Checking on up to make it only happen once
        //    if (Input.GetKeyUp(KeyCode.M))
        //    {
        //        player.transform.Rotate(0, 0, turnDict["RightTurn"].Angle);
        //    }
        //    if (Input.GetKeyUp(KeyCode.N))
        //    {
        //        player.transform.Rotate(0, 0, turnDict["LeftTurn"].Angle);
        //    }
        //}
        
    }

    public void MovePlayer(float force)
    {
        rb.AddForce(force * frontTire.transform.up);
        currVelocity = rb.velocity.magnitude;

        if (currVelocity > MAX_VELOCITY_MAG)
        {
            rb.velocity = MAX_VELOCITY_MAG * frontTire.transform.forward;
        }
    }

    public void RotatePlayer(float rotation)
    {
        if (rb.velocity != Vector2.zero)
        {
            player.transform.rotation = Quaternion.Euler(player.transform.forward * rotation + player.transform.eulerAngles);
            
            if (tireRotationFromBody > 20 && Mathf.Sign(rotation) > 0)
            {
                tireRotationFromBody = 20.0f;
            }
            else if (tireRotationFromBody < -20 && Mathf.Sign(rotation) < 0)
            {
                tireRotationFromBody = -20.0f;
            }
            
            tireRotationFromBody += rotationalSpeed * 2 * Time.deltaTime * Mathf.Sign(rotation);

            frontTire.transform.rotation = Quaternion.Euler(player.transform.forward * (tireRotationFromBody) + player.transform.eulerAngles);
        }
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
        if (collider.gameObject.tag == "Checkpoint")
        {
            return;
        }

        rb.angularVelocity = 0;
        rb.drag = 1;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Checkpoint"))
        {
            if (collider.gameObject.name.Contains("" + currCheckpoint))
            {
                currCheckpoint++;
            }

            if (collider.gameObject.name.Equals("FinishLine") && currCheckpoint == lastCheckpoint)
            {
                lap++;
                currCheckpoint = 1;
            }

            return;
        }
    }
}
