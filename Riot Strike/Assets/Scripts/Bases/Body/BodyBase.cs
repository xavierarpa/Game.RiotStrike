﻿#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dat = Environment.Data;
using XavHelpTo.Change;
using XavHelpTo;
using XavHelpTo.Get;
# endregion

/// <summary>
/// Base of every body base (ally or enemy) in game
/// </summary>
public abstract class BodyBase : MonoBehaviour
{
    #region Variables
    [Header("Character")]
    public StatData stat;
    public int life;
    public bool isDead;
    //[Space]
    //public Character character;TODO

    protected Action OnAttack;
    protected Action OnFocus;
    protected Action OnReload;
    protected Action OnSpell;
    #endregion
    #region Event

    #endregion
    #region Method

    protected virtual void Death() { }
    #endregion
}
