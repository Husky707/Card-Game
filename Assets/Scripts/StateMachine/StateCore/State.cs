using UnityEngine;
using System;

public abstract class State : MonoBehaviour
{
    public event Action Entered = delegate { };
    public event Action Exited = delegate { };
    

    public virtual void Enter() 
    {
        //Call base after custom logic
        Entered.Invoke();
    }
    public virtual void Exit() 
    {
        //Call base after your logic
        Exited.Invoke();
    }
    public virtual void Tick() { }

    public virtual void Pause() { }

    public virtual void Resume() { }
}
