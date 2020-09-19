using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWeapons : PowerUp
{
    #region MEMBERS

    public WeaponsKit weaponsKit;

    #endregion MEMBERS

    #region METHODS

    protected override void Init() { }
    protected override void ApplyBonus(Spaceship player)
    {
        player.AssignWeaponsKit(weaponsKit);
    }

    #endregion METHODS
}