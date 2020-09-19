using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    #region MEMBERS

    protected string PowerUpName;

    // [SerializeField]
    // [Tooltip("in seconds")]
    // private float selftDestructionDelay = 2.0f;

    #endregion MEMBERS

    #region METHODS

    public virtual void Start()
    {
        Init();
        Debug.Log("powerUp " + PowerUpName + " created");
    }

    void Update() { }

    protected abstract void Init();

    protected abstract void ApplyBonus(Spaceship player);

    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log("powerUp " + PowerUpName + " collision");
        Spaceship collidedPlayer = collision.gameObject.GetComponent<Spaceship>();

        if (collidedPlayer != null)
        {
            Debug.Log("powerUp colision with a player");
            ApplyBonus(collidedPlayer);
        }

        Destroy(gameObject);
    }

    #endregion METHODS

}
