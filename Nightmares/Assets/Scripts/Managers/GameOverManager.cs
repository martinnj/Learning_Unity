using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public float RestartDelay = 5f;


    Animator _anim;
    float _restartTimer;


    void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (PlayerHealth.CurrentHealth > 0) return;
        _anim.SetTrigger("GameOver");

        _restartTimer += Time.deltaTime;

        if (_restartTimer >= RestartDelay)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
