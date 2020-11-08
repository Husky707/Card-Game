using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera = null;
    public Deck<DistrictCard> playerHand = null;

    InputController Input;
    TargetController Targeter;

    #region Init
    private void Awake()
    {
        Input = gameObject.AddComponent<InputController>();
        Targeter = gameObject.AddComponent<TargetController>();
    }

    private void OnEnable()
    {
        Input.Undo += OnUndo;
        Input.LeftMouse += OnLeftMouse;
    }

    private void OnDisable()
    {
        Input.Undo -= OnUndo;
        Input.LeftMouse -= OnLeftMouse;

    }

    void Start()
    {
        
    }

    #endregion
    void Update()
    {
        
    }



    GameObject GetMouseTarget()
    {
        Ray mouseRay = playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(mouseRay, out rayHit, Mathf.Infinity))
        {
            return rayHit.transform.gameObject;
        }

        return null;
    }


    #region Input Events

    void OnLeftMouse(eInputStates mouseState)
    {
        if(mouseState == eInputStates.Down)
        {
            ITargetable possibleTarget = GetMouseTarget()?.GetComponent<ITargetable>();
            if(possibleTarget != null)
            {
                Targeter.AquireTarget(possibleTarget);

            }
        }
    }

    void OnUndo(eInputStates eventState)
    {
        Debug.Log("Got undo event");

        if (eventState == eInputStates.Down)
            Debug.Log("Undo initiated");
        if (eventState == eInputStates.Up)
            Debug.Log("Undo Ended");
    }

    #endregion
}
