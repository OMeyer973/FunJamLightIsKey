﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    #region MEMBERS

    // [SerializeField]
    // [Tooltip("in seconds")]
    // private float selftDestructionDelay = 2.0f;

    #endregion MEMBERS

    #region METHODS

    public virtual void Start()
    {
        Init();
    }

    void Update() { }

    protected abstract void Init();

    protected abstract void ApplyBonus(Spaceship player);

    protected void OnCollisionEnter(Collision collision)
    {
        Spaceship collidedPlayer = collision.gameObject.GetComponent<Spaceship>();

        if (collidedPlayer != null)
            ApplyBonus(collidedPlayer);

        Destroy(gameObject);
    }

    #endregion METHODS

}
