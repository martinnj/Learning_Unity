using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;

    private Vector3 _movement;
    private Animator _anim;
    private Rigidbody _playerRigidbody;
    private int _floorMask;
    private const float CamRayLength = 100f;
    private List<Tuple<PowerUp.PType, float, float>> _activeEffects;

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _activeEffects = new List<Tuple<PowerUp.PType, float, float>>();
    }

    // Called when we "Enter a trigger area".
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PowerUp") return;

        // Get variables from the powerup.
        var powerup = other.GetComponent<PowerUp>();
        var type = powerup.PowerAffects;
        var time = powerup.LastsFor;
        var modval = powerup.ModifierValue;

        // Apply them, and save if applicable.
        switch (type)
        {
            case PowerUp.PType.AttackSpeed:
                var player = GameObject.FindGameObjectWithTag("Player");
                var gscript = player.transform.Find("GunBarrelEnd").GetComponent<PlayerShooting>();
                gscript.TimeBetweenBullets -= modval;
                _activeEffects.Add(new Tuple<PowerUp.PType, float, float>(type, time, modval));
                break;

            case PowerUp.PType.Health:
                var hscript = transform.GetComponent<PlayerHealth>();
                hscript.CurrentHealth += (int) modval;
                break;

            case PowerUp.PType.MovementSpeed:
                Speed += modval;
                _activeEffects.Add(new Tuple<PowerUp.PType, float, float>(type, time, modval));
                break;
        }

        // Remove powerop from game
        other.gameObject.SetActive(false);
    }

    // Physics based updates.
    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
        ProcessPowerUps();
    }

    // Move Player around the world.
    void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * Speed * Time.deltaTime;

        _playerRigidbody.MovePosition(transform.position + _movement);
    }

    // Rotate the Player to aim at the mouse.
    void Turning()
    {
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (!Physics.Raycast(camRay, out floorHit, CamRayLength, _floorMask)) return;
        var playerToMouse = floorHit.point - transform.position;
        playerToMouse.y = 0f;

        var newRotation = Quaternion.LookRotation(playerToMouse);
        _playerRigidbody.MoveRotation(newRotation);
    }

    // Control the walking animation.
    void Animating(float h, float v)
    {
        var walking = h != 0f || v != 0f; //TODO: Floating point comparisons are bad :(
        _anim.SetBool("IsWalking", walking);
    }

    void ProcessPowerUps()
    {
        var expiredEffects = new List<Tuple<PowerUp.PType, float, float>>();
        foreach (var powerup in _activeEffects)
        {
            // Process effect
            var type = powerup.First;
            var life = powerup.Second;
            var modval = powerup.Third;

            powerup.Second = life - Time.deltaTime;

            if (life > 0) continue;
            // Remove the effects.
            switch (type)
            {
                // Increase cooldown of shooting.
                case PowerUp.PType.AttackSpeed:
                    var player = GameObject.FindGameObjectWithTag("Player");
                    var gscript = player.transform.Find("GunBarrelEnd").GetComponent<PlayerShooting>();
                    gscript.TimeBetweenBullets += powerup.Third;
                    break;

                case PowerUp.PType.MovementSpeed:
                    Speed -= modval;
                    break;
            }
            // Remove powerup from active effects list.
            expiredEffects.Add(powerup);
        }
        foreach (var powerup in expiredEffects)
        {
            _activeEffects.Remove(powerup);
        }
    }

}

public class Tuple<T1, T2, T3>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }
    public T3 Third { get; set; }
    internal Tuple(T1 first, T2 second, T3 third)
    {
        First = first;
        Second = second;
        Third = third;
    }
}

public static class Tuple
{
    public static Tuple<T1, T2, T3> New<T1, T2, T3>(T1 first, T2 second, T3 third)
    {
        var tuple = new Tuple<T1, T2, T3>(first, second, third);
        return tuple;
    }
}