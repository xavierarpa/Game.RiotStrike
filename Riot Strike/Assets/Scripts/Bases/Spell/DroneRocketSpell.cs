﻿#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Throw misiles who fetch the nearest target
/// </summary>
public class DroneRocketSpell : Spell
{
    #region Variable

    #endregion
    #region Event

    #endregion
    #region Method
    /// <summary>
    /// Send their rockets from the shoulder
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡
        "CAST!".Print("blue");
        //FIXME
    }
    #endregion
}
