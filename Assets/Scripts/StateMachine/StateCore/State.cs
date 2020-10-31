using UnityEngine;
using System;

public abstract class State : MonoBehaviour
{
    public event Action Entered = delegate { };
    public event Action Exited = delegate { };
    

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}
