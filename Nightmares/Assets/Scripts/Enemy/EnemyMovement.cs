using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform _player;
    PlayerHealth _playerHealth;
    EnemyHealth _enemyHealth;
    NavMeshAgent _nav;


    void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag ("Player").transform;
        _playerHealth = _player.GetComponent <PlayerHealth> ();
        _enemyHealth = GetComponent <EnemyHealth> ();
        _nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        if(_enemyHealth.CurrentHealth > 0 && _playerHealth.CurrentHealth > 0)
        {
            _nav.SetDestination (_player.position);
        }
        else
        {
            _nav.enabled = false;
        }
    }
}
