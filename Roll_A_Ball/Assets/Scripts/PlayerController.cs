using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    
	// Update is called once per frame
	void FixedUpdate ()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var movevertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, movevertical);

	    rigidbody.AddForce(movement * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);   
        }
    }
}
