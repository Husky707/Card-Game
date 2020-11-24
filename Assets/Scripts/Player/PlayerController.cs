using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandDisplay))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera = null;
    [SerializeField] DaylightState daylightRules = null;
    [SerializeField] BankController bank = null;
    public Deck<Card> playerHand = new Deck<Card>();
    private HandDisplay handDisplay = null;

    InputController Input;
    TargetController Targeter;

    #region Init
    private void Awake()
    {
        Input = gameObject.AddComponent<InputController>();
        Targeter = gameObject.AddComponent<TargetController>();

        handDisplay = GetComponent<HandDisplay>();
        handDisplay?.SetTergetDeck<Card>(playerHand);
    }

    private void OnEnable()
    {
        Input.Undo += OnUndo;
        Input.LeftMouse += OnLeftMouse;
        handDisplay.PlayRequest += PlayCard;
    }

    private void OnDisable()
    {
        Input.Undo -= OnUndo;
        Input.LeftMouse -= OnLeftMouse;
        handDisplay.PlayRequest -= PlayCard;
    }

    void Start()
    {
        
    }

    #endregion
    void Update()
    {
        
    }

    public void GainGold(int amount)
    {
        bank.PlayerGainGold(amount);
    }

    public void StealGold(int amount)
    {
        bank.PlayerGainGold(amount);
        bank.AIGainGold(-amount);
    }

    private void PlayCard(Card target)
    {
        if (!daylightRules.IsActiveState || daylightRules.CurrentTurn != eTurn.player)
        {
            Debug.Log("Not a valid time to play this card");
            return;
        }

        playerHand.Remove(target);
        target.Play();
        daylightRules.EndPlayerTurn();
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
