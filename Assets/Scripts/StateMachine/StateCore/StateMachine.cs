using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState => _currentState;
    public bool InTransition { get; private set; } = false;

    private State _currentState;
    protected State previousState;
    private State queuedState;

    protected virtual void Update()
    {
        if(CurrentState != null && !InTransition)
        {
            CurrentState.Tick();
        }
    }

    public void ChangeState<T>(GameObject fromObj) where T: State
    {
        T targetState = fromObj.GetComponent<T>();
        if(targetState == null)
        {
            Debug.Log("Could not find target state on the " + fromObj.name + " game object");
            return;
        }

        BeginStateChange(targetState);
    }
    public void ChangeState<T>() where T: State
    {
        ChangeState<T>(this.gameObject);
    }
    public void ChangeState(State toState)
    {
        BeginStateChange(toState);
    }

    public void RevertState()
    {
        if (previousState == null)
            return;

        BeginStateChange(previousState);
    }



    #region Methods
    private void BeginStateChange(State toState)
    {
        if(_currentState != toState && !InTransition)
        {
            Transition(toState);
        }
    }

    private void Transition(State toState)
    {
        InTransition = true;
        queuedState = toState;

        if(_currentState == null)
        {
            //Case for entering the very first state
            EnterNewState(toState);
        }
        else
        {
            _currentState.Exited += OnStateExited;
            _currentState.Exit();
        }
    }

    private void OnStateExited( )
    {
        _currentState.Exited -= OnStateExited;
        previousState = _currentState;
        _currentState = null;

        EnterNewState(queuedState);
    }

    private void OnStateEntered()
    {
        _currentState = queuedState;
        queuedState.Entered -= OnStateEntered;
        queuedState = null;

        InTransition = false;
    }

    private void EnterNewState(State state)
    {
        if(state == null)
        {
            Debug.Log("State died in queue.");
            return;
        }

        queuedState = state;
        state.Entered += OnStateEntered;
        state.Enter();
    }
    #endregion
}
