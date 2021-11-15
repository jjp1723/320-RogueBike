using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCard : MonoBehaviour
{
    [SerializeField]
    private float angle;

    public float Angle{
        get {
            return angle;
        }
    }
}
