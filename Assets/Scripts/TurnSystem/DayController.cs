using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DaySM))]
public class DayController : MonoBehaviour
{

    public DaySM StateMachine { get; private set; }

    #region Init
    private void Awake()
    {
        StateMachine = GetComponent<DaySM>();
    }

    #endregion



}
