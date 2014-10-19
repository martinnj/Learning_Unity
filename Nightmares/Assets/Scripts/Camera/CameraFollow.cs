using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float Smoothing = 5f;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - Target.position;
    }

    // Physics steps.
    void FixedUpdate()
    {
        var targetCamPos = Target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
    }
}
