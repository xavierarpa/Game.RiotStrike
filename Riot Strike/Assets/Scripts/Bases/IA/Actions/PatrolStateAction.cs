﻿#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Know;
using XavHelpTo.Look;

#endregion
/// <summary>
/// Follow the object and their childs to do the movement
/// </summary>
[CreateAssetMenu(menuName = "IA/Action/Patrol")]
public class PatrolStateAction : StateAction
{
    #region Variables
    [Header("Patrol Action")]
    public bool isRandom = false;
    public float distanceWithTarget = 1f;

    #endregion
    #region Methods
    /// <summary>
    /// Do the Patrol
    /// </summary>
    public override void Act(IABody ia) {

        if (!ia.initedPatrol) InitState(ia);
        Patrol(ia);
    }
    /// Initializes the status of the patrol
    /// </summary>
    private void InitState(IABody ia){
        ia.initedPatrol = true;
        ia.indexPatrol = PatrolQty(ia).ZeroMax();
    }

    /// <summary>
    /// Patrol who where is he in the  state
    /// </summary>
    private void Patrol(IABody ia)
    {
        //Do the patrol
        ia.Move(PatrolPosition(ia));
        ia.Rotate(PatrolPosition(ia));
        CheckForNextPosition(ia);
    }
    /// <summary>
    /// Check if is at the end of the current position and choose the next
    /// it can be random or not
    /// </summary>
    private void CheckForNextPosition(IABody ia) {
        float distance = Vector3.Distance(
            ia.transform.position,
            PatrolPosition(ia)
        );
        if (distance < distanceWithTarget)
        {
            ia.indexPatrol = isRandom 
                ? PatrolQty(ia).DifferentIndex(ia.indexPatrol)
                : Know.NextIndex(true, PatrolQty(ia) , ia.indexPatrol)
            ;
        }
    }

    
    /// <summary>
    /// get the position of the patrol
    /// </summary>
    private Vector3 PatrolPosition(IABody ia) => ia.patrol.GetChild(ia.indexPatrol).position;
    /// <summary>
    /// Qty of childs
    /// </summary>
    private int PatrolQty(IABody ia) => ia.patrol.childCount;
    #endregion
}
