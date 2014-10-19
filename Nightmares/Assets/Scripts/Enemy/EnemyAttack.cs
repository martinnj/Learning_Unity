using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float TimeBetweenAttacks = 0.5f;
    public int AttackDamage = 10;


    Animator _anim;
    GameObject _player;
    PlayerHealth _playerHealth;
    EnemyHealth _enemyHealth;
    bool _playerInRange;
    float _timer;


    void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag ("Player");
        _playerHealth = _player.GetComponent <PlayerHealth> ();
        _enemyHealth = GetComponent<EnemyHealth>();
        _anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject != _player) return;
        _playerInRange = true;
    }


    void OnTriggerExit (Collider other)
    {
        if (other.gameObject != _player) return;
        _playerInRange = false;
    }


    void Update ()
    {
        _timer += Time.deltaTime;

        if(_timer >= TimeBetweenAttacks && _playerInRange && _enemyHealth.CurrentHealth > 0)
        {
            Attack ();
        }

        if (_playerHealth.CurrentHealth > 0) return;
        _anim.SetTrigger ("PlayerDead");
    }


    void Attack ()
    {
        _timer = 0f;

        if (_playerHealth.CurrentHealth <= 0) return;
        _playerHealth.TakeDamage (AttackDamage);
    }
}
