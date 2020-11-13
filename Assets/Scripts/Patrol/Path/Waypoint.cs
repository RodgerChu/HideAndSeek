using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private float _waypointHoldTime = 2f;

    [SerializeField, Tooltip("According to global X")]
    private FaceDirection _onHoldFaceDirection = FaceDirection.RIGHT;

    public float WaypointHoldTime
    {
        get => _waypointHoldTime;
    }

    public Vector3 OnHoldVectorFaceDirection
    {
        get
        {
            switch (_onHoldFaceDirection)
            {
                case FaceDirection.LEFT:
                    return new Vector3(0, 0, 1);
                case FaceDirection.RIGHT:
                    return new Vector3(0, 0, -1);
                case FaceDirection.FORWARD:
                    return new Vector3(1, 0, 0);
                default:
                    return new Vector3(-1, 0, 0);
            }
        }
    }

    public enum FaceDirection
    {
        LEFT, RIGHT, FORWARD, BACKWARD
    }
}
