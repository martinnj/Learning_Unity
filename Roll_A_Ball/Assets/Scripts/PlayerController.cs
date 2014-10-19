using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public Text CountText;
    public Text WinText;
    private int _count;

    // Called when object is created.
    void Start()
    {
        _count = 0;
       SetCountText();
        WinText.text = "";
    }

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
        if (other.gameObject.tag != "PickUp") return;
        other.gameObject.SetActive(false);
        _count++;
        SetCountText();
    }

    void SetCountText()
    {
        CountText.text = "Count: " + _count;
        if (_count >= 10)
        {
            WinText.text = "You won! :D";
        }
    }
}
