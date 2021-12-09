using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController ai;

    private float rotationalSpeed;
    private float forwardForceMagnitude;
    private float brakeForceMagnitude;

    [SerializeField]
    private GameObject nodeParent;
    private GameObject[] nodes;
    private int curNode;

    private float targetAngle;
    private Vector2 targetDir;

    private Vector2 curPos;
    private Vector2 targetPos;

    private float angle; ///<difference between current direction and target direction

    private float radius;

    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotationalSpeed = ai.RotationalSpeed;
        forwardForceMagnitude = ai.ForwardForceMagnitude;
        brakeForceMagnitude = ai.BrakeForceMagnitude;

        curNode = 0;

        int childLength = nodeParent.transform.childCount;

        nodes = new GameObject[childLength];

        for (int i = 0; i < childLength; i++) {
            nodes[i] = nodeParent.gameObject.transform.GetChild(i).gameObject;
        }

        radius = ai.GetComponent<CircleCollider2D>().radius;

        UpdateDir();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime + Time.deltaTime < 3.1)
        {
            waitTime += Time.deltaTime;
            return;
        }

        float targetDist = (Mathf.Pow(curPos.x - targetPos.x, 2) + Mathf.Pow(curPos.y - targetPos.y, 2));

        if (targetDist < 4)
        {
            ai.MovePlayer(brakeForceMagnitude * Time.deltaTime);
        }
        else
        {
            ai.MovePlayer(forwardForceMagnitude * Time.deltaTime);
        }

        UpdateDir();

        float targetRadius = nodes[curNode].transform.localScale.x;
        if (targetDist <= Mathf.Pow(targetRadius + ai.transform.localScale.x, 2))
        {
            //Debug.Log("Hit Node "+ curNode);
            if (curNode < nodes.Length-1)
            {
                curNode++;
            }
            else
            {
                curNode = 0;
            }
            UpdateDir();
        }


        //Debug.Log("rotate" + rotate);
        //ai.RotatePlayer(rotate * Time.deltaTime);
        //target is below and to right of ai so turn left (away)

        Vector2 curDir = Vector3.Normalize(ai.transform.up);
        angle = Vector3.Dot(targetDir, ai.transform.right);

        //float rotate = 0;
        
        //target is to the right
        if (angle > 0)
        {
            ai.RotatePlayer(-1 * rotationalSpeed * Time.deltaTime);
        }
        else if(angle < 0)
        {
            ai.RotatePlayer(rotationalSpeed * Time.deltaTime);
        }
      
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
