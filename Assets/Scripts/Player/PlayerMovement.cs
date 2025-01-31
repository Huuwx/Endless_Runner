using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private int currentLane = 1;
    private int desiredLane = 1;
    public float laneDistance = 3;

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
        if (PlayerController.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            if (SwipeManager.swipeUp && PlayerController.Instance.GetIsGrounded() == true)
            {
                Jump();
            }

            if (SwipeManager.swipeLeft)
            {
                if (PlayerController.Instance.GetIsGrounded())
                {
                    SoundController.Instance.PlayOneShot(SoundController.Instance.dash);
                    PlayerController.Instance.animator.SetTrigger("swipeLeft");
                }
                desiredLane--;
                currentLane = desiredLane + 1;
                if(desiredLane == -1)
                {
                    desiredLane = 0;
                    currentLane = 0;
                }
            }
            else if (SwipeManager.swipeRight)
            {
                if (PlayerController.Instance.GetIsGrounded())
                {
                    SoundController.Instance.PlayOneShot(SoundController.Instance.dash);
                    PlayerController.Instance.animator.SetTrigger("swipeRight");
                }
                desiredLane++;
                currentLane = desiredLane - 1;
                if (desiredLane == 3)
                {
                    desiredLane = 2;
                    currentLane = 2;
                }
            }

            Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

            if(desiredLane == 0)
            {
                targetPos += Vector3.left * laneDistance;
            }else if(desiredLane == 2)
            {
                targetPos += Vector3.right * laneDistance;
            }

            //transform.position = Vector3.Lerp(targetPos, targetPos, 80 * Time.deltaTime);

            if (transform.position != targetPos)
            {
                Vector3 diff = targetPos - transform.position;
                Vector3 moveDir = diff.normalized * 35 * Time.deltaTime;
                if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                    rb.MovePosition(transform.position + moveDir);
                else
                    rb.MovePosition(targetPos);
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
    }

    private void Jump()
    {
        SoundController.Instance.PlayOneShot(SoundController.Instance.jump);
        PlayerController.Instance.animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void BackToCurrentLane()
    {
        desiredLane = currentLane;
    }

    public void Bounce()
    {
        rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
    }
}
