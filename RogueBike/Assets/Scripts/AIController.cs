using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController ai;

    private float rotationalSpeed;
    private float forwardForceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        rotationalSpeed = ai.RotationalSpeed;
        forwardForceMagnitude = ai.ForwardForceMagnitude;

    }

    // Update is called once per frame
    void Update()
    {
        ai.MovePlayer(forwardForceMagnitude * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 targetPos = collision.transform.position;
        Vector2 curPos = ai.transform.position;

        //target is below and to right of ai so turn left (away)
        if (targetPos.x < curPos.x && targetPos.y < curPos.y)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
        //target is below and to left of ai
        else if (targetPos.x > curPos.x && targetPos.y < curPos.y)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);

        }
        //target is above and to left of ai
        else if (targetPos.x < curPos.x && targetPos.y > curPos.y)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
        //target is above and to right of ai so turn right (away)
        else if (targetPos.x > curPos.x && targetPos.y > curPos.y)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 targetPos = collision.transform.position;
        Vector2 curPos = ai.transform.position;
        
        //target is below and to right of ai so turn left (away)
        if (targetPos.x < curPos.x && targetPos.y < curPos.y)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
        //target is below and to left of ai
        else if(targetPos.x > curPos.x && targetPos.y < curPos.y)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);

        }
        //target is above and to left of ai
        else if(targetPos.x < curPos.x && targetPos.y > curPos.y)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        }        
        //target is above and to right of ai so turn right (away)
        else if (targetPos.x > curPos.x && targetPos.y > curPos.y)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
    }
}
