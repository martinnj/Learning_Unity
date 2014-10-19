using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject Player;
    private Vector3 _offset;

	// Use this for initialization
	void Start ()
	{
	    _offset = transform.position;
	}
	
	
	void LateUpdate ()
	{
	    transform.position = Player.transform.position + _offset;
	}
}
