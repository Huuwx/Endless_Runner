using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float speed = 20f;
    private float horizontalInput;
    //Jump
    [SerializeField] float jumpForce = 100f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && PlayerController.Instance.GetIsGrounded() == true) 
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance.GetIsAlive() == true)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + horizontalMove + forwardMove);
        }
    }

    private void Jump()
    {
        PlayerController.Instance.animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jumpForce);
    }
}
