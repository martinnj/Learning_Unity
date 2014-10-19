using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;

    private Vector3 _movement;
    private Animator _anim;
    private Rigidbody _playerRigidbody;
    private int _floorMask;
    private const float CamRayLength = 100f;

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // Physics based updates.
    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * Speed * Time.deltaTime;

        _playerRigidbody.MovePosition(transform.position + _movement);
    }

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

    void Animating(float h, float v)
    {
        var walking = h != 0f || v != 0f; //TODO: Floating point comparisons are bad :(
        _anim.SetBool("IsWalking", walking);
    }

}
