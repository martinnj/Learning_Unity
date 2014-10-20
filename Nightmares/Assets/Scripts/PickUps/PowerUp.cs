using System;
using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public enum PType
    {
        AttackSpeed,
        MovementSpeed,
        Health
    };

    public PType PowerAffects;  // What sort of powerup is this?
    public Color PColor;        // Base color of the powerup material.
    public float ModifierValue; // Value to modify with.
    public float LifeTime;      // How long the powerup stays alive if not picked up.
    public float LastsFor;      // How long the powerup will affect the player.

    private float _alphaStep;

    // Use this for initialization
    void Start ()
    {
        renderer.material.color = PColor;
        _alphaStep = PColor.a / LifeTime;
        Debug.Log("Alpha Falloff rate:" + _alphaStep);
    }
	
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    // Called once every physics tick.
    void FixedUpdate()
    {
        PColor.a -= _alphaStep;
        renderer.material.color = PColor;

        LifeTime -= Time.deltaTime;

        // Dispose of the PU if it's not picked up.
        if (LifeTime != 0) return;
        Destroy(transform.gameObject);
    }

    void PrintVars()
    {
        Debug.Log("ModifierValue: " + ModifierValue + "\n" + 
                  "PColor: " + PColor + "\n" +
                  "LifeTime: " + LifeTime + "\n" +
                  "EffectTimer: " + LastsFor + "\n" +
                  "_alphaStep: " + _alphaStep);
    }
}
