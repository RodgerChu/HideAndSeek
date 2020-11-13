using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelFinish : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onFinishReachedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            _onFinishReachedEvent?.Invoke();
    }
}
