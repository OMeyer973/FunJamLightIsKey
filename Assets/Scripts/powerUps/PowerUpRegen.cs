using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRegen : PowerUp
{
    #region MEMBERS

    public int healthBonus = 20;

    #endregion MEMBERS

    #region METHODS

    public override void Start()
    {
        base.Start();
    }

    void Update() { }

    protected override void Init()
    {
        PowerUpName = "Regen";
    }
    protected override void ApplyBonus(Spaceship player)
    {
        player.Regen(10);
    }

    #endregion METHODS
}