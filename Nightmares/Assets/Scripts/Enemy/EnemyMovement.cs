using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform _player;
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    NavMeshAgent _nav;


    void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag ("Player").transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        _nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            _nav.SetDestination (_player.position);
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
}
