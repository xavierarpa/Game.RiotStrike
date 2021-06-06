﻿#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Set;
using XavHelpTo.Get;
using Dat = Environment.Data;
#endregion

/// <summary>
/// Management of the body who player can `play`
/// </summary>
public class PlayerBody : Body
{
    #region Variable
    private const string KEY_AXIS_X = "Mouse X";
    private const string KEY_AXIS_Y = "Mouse Y";
    [Header("Player Body")]
    public HUDController ctrl_HUD;
    public bool canMove = true;
    public bool canRotate = true;

    #endregion
    #region Event
    public override void Awake() {
        base.Awake();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        Subscribes();
    }
    public override void Start()
    {
        base.Start();
        EmitLife();
        EmitShotCursor();
    }
    private void Update()
    {
        Control();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        UnSubscribes();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Do the subscriptions
    /// </summary>
    private void Subscribes()
    {
        character.spell.OnTimer += ctrl_HUD.RefreshSpell;
        character.weapon.OnFireAttack += ctrl_HUD.RefreshWeapon;
        character.weapon.OnReload += ctrl_HUD.RefreshReload;
        character.weapon.OnTargetImpactWeapon += ctrl_HUD.CreateDamageText;
        OnChangeLife += EmitLife;
    }
    /// <summary>
    /// Do the UnSubscriptions
    /// </summary>
    private void UnSubscribes()
    {
        character.spell.OnTimer -= ctrl_HUD.RefreshSpell;
        character.weapon.OnFireAttack -= ctrl_HUD.RefreshWeapon;
        character.weapon.OnReload -= ctrl_HUD.RefreshReload;
        character.weapon.OnTargetImpactWeapon -= ctrl_HUD.CreateDamageText;
        OnChangeLife -= EmitLife;
    }
    /// <summary>
    /// Emit the Life status in UI
    /// </summary>
    private void EmitLife() => ctrl_HUD.RefreshLife(life, stat.DEFENSE);
    /// <summary>
    /// Emit the new shot cursor in UI
    /// </summary>
    private void EmitShotCursor() => ctrl_HUD.RefreshShotCursor(character.weapon.ID);
    /// <summary>
    /// Controls the actions of the player
    /// </summary>
    private void Control()
    {
        //MOVEMENT
        if (canMove) movement.Move(stat.SPEED,
            Utils.Axis(EControl.RIGHT, EControl.LEFT),
            0f,
            Utils.Axis(EControl.FORWARD, EControl.BACK)
        );

        //ROTATION
        if (canRotate) rotation.Rotate(
            Utils.Axis(KEY_AXIS_X, ESwitchOpt.INVERT_AXIS_X, stat.SPEED),
            Utils.Axis(KEY_AXIS_Y, ESwitchOpt.INVERT_AXIS_Y, stat.SPEED)
        );

        //ATTACK
        CheckPress(EControl.ATTACK, character.OnAttack, this);

        //FOCUS
        CheckPress(EControl.AIM, character.OnAim, this);
        //DISFOCUS
        CheckPressUp(EControl.AIM, character.OnAim, this);

        //RELOAD
        CheckPressDown(EControl.RELOAD, character.OnReload);

        //SPELL
        CheckPressDown(EControl.CAST, character.OnCast, this);

        //CHAT
        // TODO (Multiplayer) 

        //PAUSE
        // TODO
    }

    /// <summary>
    /// fire the <seealso cref="Action"/> if <seealso cref="EControl"/> is pressed
    /// </summary>
    private void CheckPress(EControl e, Action a){if (e.IsPressed()) a?.Invoke();}
    /// <summary>
    /// Check if the <seealso cref="EControl"/> is pressing and send the <seealso cref="Action"/>
    /// </summary>
    private void CheckPress<T>(EControl e, Action<T> a, T val){if (e.IsPressed()) a?.Invoke(val);}
    /// <summary>
    /// as <seealso cref="CheckPress(EControl, Action)"/> just in the frame
    /// </summary>
    private void CheckPressDown(EControl e, Action a){if (e.IsPressedDown()) a?.Invoke();}
    /// <summary>
    /// as <seealso cref="CheckPress(EControl, Action)"/> just in the frame
    /// Check if the <seealso cref="EControl"/> was pressed and send the <seealso cref="Action"/>
    /// </summary>
    private void CheckPressDown<T>(EControl e, Action<T> a, T val){ if (e.IsPressedDown()) a?.Invoke(val);}
    /// <summary>
    /// as <seealso cref="CheckPress(EControl, Action)"/> just in the frame
    /// Check if the <seealso cref="EControl"/> was pressed and send the <seealso cref="Action"/>
    /// </summary>
    private void CheckPressUp<T>(EControl e, Action<T> a, T val) { if (e.IsPressedUp()) a?.Invoke(val); }

    /// <summary>
    /// Adds positive or negative life
    /// </summary>
    public override void AddLife(int value)
    {
        base.AddLife(value);
        EmitLife();
    }
    #endregion

}
