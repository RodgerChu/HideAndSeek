using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressEvents : MonoBehaviour
{
    private Action _onPlayerSpotted;
    private Action _onFinishReached;

    public void AddOnPlayerSpottedHandler(Action handler)
    {
        _onPlayerSpotted += handler;
    }

    public void RemoveOnPlayerSpottedHandler(Action handler)
    {
        _onPlayerSpotted -= handler;
    }

    public void AddOnPlayerReachedFinishHandler(Action handler)
    {
        _onFinishReached += handler;
    }

    public void RemoveOnPlayerReachedFinishHandler(Action handler)
    {
        _onFinishReached += handler;
    }

    public void FirePlayerSpottedEvent()
    {
        _onPlayerSpotted?.Invoke();
    }

    public void FirePlayerFinishReachedEvent()
    {
        _onFinishReached?.Invoke();
    }
}
