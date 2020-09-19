using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Spaceship parentSpaceship;
    private Spaceship target;
    // Start is called before the first frame update
    public void Initiate (Spaceship parentSpaceship, Spaceship target)
    {
        if (parentSpaceship) this.parentSpaceship = parentSpaceship;
        else Debug.Log("wrong parentSpaceship in turret initiate");
        if (target) this.target = target; 
        else Debug.Log("wrong target spaceship in turret initiate");
    }
    
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public void Aim() { if (target) transform.LookAt(target.transform, Vector3.up); }

    public void Shoot(Missile missile)
    {
        Missile missileShot = Instantiate(missile, transform.position + transform.forward * .8f, transform.rotation);
        missileShot.Initiate(parentSpaceship, target);
    }
}
