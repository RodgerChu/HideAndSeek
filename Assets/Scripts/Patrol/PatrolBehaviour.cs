using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    private PatrolPath _patrolPath;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Animator _animator;

    private Vector3 _prevPosition = Vector3.zero;

    private Waypoint _currentWaypoint;

    private bool _holding = false;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {


        if (!_holding && _agent.remainingDistance < _agent.stoppingDistance)
        {
            _holding = true;

            var lookTarget = transform.position + _currentWaypoint.OnHoldVectorFaceDirection;
            _agent.SetDestination(lookTarget);

            Debug.Log("set next waypoint");

            var nextWaypoint = _patrolPath.GetNextWaypoint();
            StartCoroutine(AssignNextWaypoint(nextWaypoint));
        }

        _animator.SetFloat("movespeed", GetMovespeed());
    }

    private IEnumerator AssignNextWaypoint(Waypoint nextWaypoint)
    {
        yield return new WaitForSeconds(_currentWaypoint?.WaypointHoldTime ?? 0f);
        _currentWaypoint = nextWaypoint;

        _holding = false;

        _agent.SetDestination(_currentWaypoint.transform.position);
    }

    public void ResetPatrolBehaviour()
    {
        _patrolPath.ReserPath();
        Reset();
    }

    private void Reset()
    {
        var startPoint = _patrolPath.GetFirstWaypoint().transform.position;
        gameObject.transform.position = new Vector3(startPoint.x, gameObject.transform.position.y, startPoint.z);

        _currentWaypoint = _patrolPath.GetNextWaypoint();
        _agent.SetDestination(_currentWaypoint.transform.position);
    }

    private float GetMovespeed()
    {
        if (_prevPosition == Vector3.zero)
        {
            _prevPosition = transform.position;
            return 0f;
        }
        Vector3 curMove = transform.position - _prevPosition;
        _prevPosition = transform.position;
        return curMove.magnitude / Time.deltaTime;
    }
}
