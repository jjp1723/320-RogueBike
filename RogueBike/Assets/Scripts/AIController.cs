using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController ai;

    private float rotationalSpeed;
    private float forwardForceMagnitude;

    [SerializeField]
    private GameObject[] nodes;
    private int curNode;

    private float targetAngle;
    private Vector2 targetDir;

    private Vector2 curPos;
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rotationalSpeed = ai.RotationalSpeed;
        forwardForceMagnitude = ai.ForwardForceMagnitude;

        curNode = 0;

        UpdateDir();
    }

    // Update is called once per frame
    void Update()
    {
        ai.MovePlayer(forwardForceMagnitude * Time.deltaTime);

        float radius = nodes[curNode].transform.localScale.x;
        if ((Mathf.Pow(curPos.x - targetPos.x, 2) + Mathf.Pow(curPos.y - targetPos.y, 2)) <= Mathf.Pow(radius + ai.transform.localScale.x, 2))
        {
            Debug.Log("Hit Node "+ curNode);
            if (curNode < nodes.Length-1)
            {
                curNode++;
            }
            else
            {
                curNode = 0;
            }
        }

        UpdateDir();

        float rotate = Mathf.Min(rotationalSpeed, Vector2.Angle(targetDir, ai.transform.up));
        //Debug.Log("rotate" + rotate);
        //ai.RotatePlayer(rotate * Time.deltaTime);
        Vector2 curDir = ai.transform.up;
        //target is below and to right of ai so turn left (away)

        //it's current direction is more to the right than it should so turn left
        if(targetDir.x > curDir.x)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        }
        if(targetDir.x < curDir.x)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
        //else if(targetDir.y > curDir.y)
        //{
        //    ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        //}
        //else if (targetDir.y < curDir.y)
        //{
        //    ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        //}
        //if (targetDir.x < curPos.x && targetDir.y < curPos.y)
        //{
        //    ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        //}
        ////target is below and to left of ai
        //else if (targetDir.x > curPos.x && targetDir.y < curPos.y)
        //{
        //    ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);

        //}
        ////target is above and to left of ai
        //else if (targetDir.x < curPos.x && targetDir.y > curPos.y)
        //{
        //    ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        //}
        ////target is above and to right of ai so turn right (away)
        //else if (targetDir.x > curPos.x && targetDir.y > curPos.y)
        //{
        //    ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        //}
    }

    private void UpdateDir()
    {
        curPos = ai.transform.position;
        targetPos = nodes[curNode].transform.position;

        targetDir = Vector3.Normalize(targetPos - curPos);
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Vector2 targetPos = collision.transform.position;
    //    Vector2 curPos = ai.transform.position;

    //    //target is below and to right of ai so turn left (away)
    //    if (targetPos.x < curPos.x && targetPos.y < curPos.y)
    //    {
    //        ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
    //    }
    //    //target is below and to left of ai
    //    else if (targetPos.x > curPos.x && targetPos.y < curPos.y)
    //    {
    //        ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);

    //    }
    //    //target is above and to left of ai
    //    else if (targetPos.x < curPos.x && targetPos.y > curPos.y)
    //    {
    //        ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
    //    }
    //    //target is above and to right of ai so turn right (away)
    //    else if (targetPos.x > curPos.x && targetPos.y > curPos.y)
    //    {
    //        ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Vector2 targetPos = collision.transform.position;
    //    Vector2 curPos = ai.transform.position;
        
    //    //target is below and to right of ai so turn left (away)
    //    if (targetPos.x < curPos.x && targetPos.y < curPos.y)
    //    {
    //        ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
    //    }
    //    //target is below and to left of ai
    //    else if(targetPos.x > curPos.x && targetPos.y < curPos.y)
    //    {
    //        ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);

    //    }
    //    //target is above and to left of ai
    //    else if(targetPos.x < curPos.x && targetPos.y > curPos.y)
    //    {
    //        ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
    //    }        
    //    //target is above and to right of ai so turn right (away)
    //    else if (targetPos.x > curPos.x && targetPos.y > curPos.y)
    //    {
    //        ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
    //    }
    //}
}
