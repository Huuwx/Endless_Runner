using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject lane;

    [SerializeField] float speed = 20f;
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
        if(Input.GetKeyDown(KeyCode.Space) && PlayerController.Instance.GetIsGrounded() == true) 
        {
            Jump();
        }
        if (PlayerController.Instance.GetIsAlive() == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Get current lane index
                int currentLane = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (Mathf.Approximately(transform.position.x, lane.transform.GetChild(i).position.x))
                    {
                        currentLane = i;
                        break;
                    }
                }

                // Move left if not in leftmost lane
                if (currentLane > 0)
                {
                    Vector3 newPos = transform.position;
                    newPos.x = lane.transform.GetChild(currentLane - 1).position.x;
                    transform.position = newPos;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Get current lane index
                int currentLane = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (Mathf.Approximately(transform.position.x, lane.transform.GetChild(i).position.x))
                    {
                        currentLane = i;
                        break;
                    }
                }

                // Move right if not in rightmost lane
                if (currentLane < 2)
                {
                    Vector3 newPos = transform.position;
                    newPos.x = lane.transform.GetChild(currentLane + 1).position.x;
                    transform.position = newPos;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance.GetIsAlive() == true)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
    }

    private void Jump()
    {
        PlayerController.Instance.animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jumpForce);
    }
}
