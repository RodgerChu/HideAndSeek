using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField]
    private Waypoint[] _waypoints;

    private uint _currentWaypointIndex = 0;

    public Waypoint GetNextWaypoint()
    {
        _currentWaypointIndex++;

        if (_currentWaypointIndex >= _waypoints.Length)
            _currentWaypointIndex = 0;

        return _waypoints[_currentWaypointIndex];
    }

    public Waypoint GetFirstWaypoint()
    {
        return _waypoints[0];
    }

    public void ReserPath()
    {
        _currentWaypointIndex = 0;
    }
}
