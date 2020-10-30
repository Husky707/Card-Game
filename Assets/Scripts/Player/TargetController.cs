using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTargetTypes { Any, Card, Pawn }

public class TargetController : MonoBehaviour
{
    public static ITargetable CurrnetTarget = null;
   

    public void AquireTarget(ITargetable possibleTarget, eTargetTypes constraint = eTargetTypes.Any)
    {
        if (possibleTarget == null)
            return;

        if(constraint == eTargetTypes.Any )// || Targets match)
        {
            CurrnetTarget = possibleTarget;
            CurrnetTarget.Target();        
        }
    }
}
