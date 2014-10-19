using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100; // If this value gets updated, the healthslider should be updated as well.
    public int CurrentHealth;
    public Slider HealthSlider;
    public Image DamageImage;
    public AudioClip DeathClip;
    public float FlashSpeed = 5f;
    public Color FlashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator _anim;
    AudioSource _playerAudio;
    PlayerMovement _playerMovement;
    PlayerShooting _playerShooting;
    bool _isDead;
    bool _damaged;


    void Awake ()
    {
        _anim = GetComponent <Animator> ();
        _playerAudio = GetComponent <AudioSource> ();
        _playerMovement = GetComponent <PlayerMovement> ();
        _playerShooting = GetComponentInChildren <PlayerShooting> ();
        CurrentHealth = StartingHealth;
    }


    void Update ()
    {
        if(_damaged)
        {
            DamageImage.color = FlashColour;
        }
        else
        {
            DamageImage.color = Color.Lerp (DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        _damaged = false;
    }


    public void TakeDamage (int amount)
    {
        _damaged = true;

        CurrentHealth -= amount;

        HealthSlider.value = CurrentHealth;

        _playerAudio.Play ();

        if(CurrentHealth <= 0 && !_isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        _isDead = true;

        _playerShooting.DisableEffects ();

        _anim.SetTrigger ("Die");

        _playerAudio.clip = DeathClip;
        _playerAudio.Play ();

        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }
}
