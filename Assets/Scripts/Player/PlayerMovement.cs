using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    BoxCollider boxCollider;
    private Rigidbody rb;

    public int oldLane = 1;
    public int desiredLane = 1;
    private float center = 0;
    public float centerZ = -20;

    public float laneDistance = 3;

    [SerializeField] float speed = 0f;
    //Jump
    [SerializeField] float jumpForce = 100f;

    [SerializeField] LayerMask turnLayer;

    public bool isZPositive = true;

    private void Awake()
    {
        center = 0;
        isZPositive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = rb.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            CheckTurn();

            if (SwipeManager.swipeUp && PlayerController.Instance.GetIsGrounded() == true)
            {
                ResetCollider();
                Jump();
            }

            if (SwipeManager.swipeLeft)
            {
                if (PlayerController.Instance.GetIsGrounded())
                {
                    ResetCollider();
                    SoundController.Instance.PlayOneShot(SoundController.Instance.dash);
                    PlayerController.Instance.animator.SetTrigger("swipeLeft");
                }
                desiredLane--;
                oldLane = desiredLane + 1;
                if (desiredLane == -1)
                {
                    desiredLane = 0;
                    oldLane = 0;
                }
            }
            else if (SwipeManager.swipeRight)
            {
                if (PlayerController.Instance.GetIsGrounded())
                {
                    ResetCollider();
                    SoundController.Instance.PlayOneShot(SoundController.Instance.dash);
                    PlayerController.Instance.animator.SetTrigger("swipeRight");
                }
                desiredLane++;
                oldLane = desiredLane - 1;
                if (desiredLane == 3)
                {
                    desiredLane = 2;
                    oldLane = 2;
                }
            }

            if (SwipeManager.swipeDown)
            {
                StartCoroutine(Slide());
            }

            Vector3 targetPos = new Vector3(0, 0, 0);

            if (isZPositive)
            {
                targetPos = new Vector3(center, transform.position.y, transform.position.z);

                if (desiredLane == 0)
                {
                    if (center >= 0)
                    {
                        targetPos += Vector3.left * laneDistance;
                    }
                    else
                    {
                        targetPos += Vector3.right * laneDistance;
                    }
                }
                else if (desiredLane == 2)
                {
                    if (center >= 0)
                    {
                        targetPos += Vector3.right * laneDistance;
                    }
                    else 
                    {
                        targetPos += Vector3.left * laneDistance;
                    }
                }
            }
            else
            {
                targetPos = new Vector3(transform.position.x, transform.position.y, centerZ);

                if (desiredLane == 0)
                {
                    if (centerZ < 0)
                    {
                        targetPos += Vector3.forward * laneDistance;
                    }
                    else
                    {
                        targetPos += Vector3.back * laneDistance;
                    }
                }
                else if (desiredLane == 2)
                {
                    if (centerZ < 0)
                    {
                        targetPos += Vector3.back * laneDistance;
                    }
                    else
                    {
                        targetPos += Vector3.forward * laneDistance;
                    }
                }
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, 20 * Time.deltaTime);

            //if (transform.position != targetPos)
            //{
            //    Vector3 diff = targetPos - transform.position;
            //    Vector3 moveDir = diff.normalized * 35 * Time.deltaTime;
            //    if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            //        rb.MovePosition(transform.position + moveDir);
            //    else
            //        rb.MovePosition(targetPos);
            //}
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = new Vector3(0, 0, 0) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }


    private Vector3? CheckTurn()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, turnLayer);
        if (hitColliders.Length != 0)
        {
            TurnGroundController turnGroundController = hitColliders[0].GetComponent<TurnGroundController>();
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isZPositive = false;
                transform.position = turnGroundController.pivot.position;
                centerZ = turnGroundController.pivot.position.z;
                turnGroundController.spawner.moveDirection = new Vector3(-1, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                desiredLane = 1;
                oldLane = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isZPositive = true;
                transform.position = turnGroundController.pivot.position;
                center = turnGroundController.pivot.position.x;
                turnGroundController.spawner.moveDirection = new Vector3(0, 0, -1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                desiredLane = 1;
                oldLane = 1;
            }
        }
        return null;
    }

    private void Jump()
    {
        SoundController.Instance.PlayOneShot(SoundController.Instance.jump);
        PlayerController.Instance.animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private IEnumerator Slide()
    {
        boxCollider.size = new Vector3(1, 0.5f, 1);
        boxCollider.center = new Vector3(0, 0.25f, 0);
        PlayerController.Instance.animator.SetBool("isSliding", true);

        yield return new WaitForSeconds(1f);

        boxCollider.size = new Vector3(1, 2.5f, 1);
        boxCollider.center = new Vector3(0, 1.1f, 0);
        PlayerController.Instance.animator.SetBool("isSliding", false);
    }

    public void ResetCollider()
    {
        boxCollider.size = new Vector3(1, 2.5f, 1);
        boxCollider.center = new Vector3(0, 1.1f, 0);
        PlayerController.Instance.animator.SetBool("isSliding", false);
    }

    public void BackToOldLane()
    {
        desiredLane = oldLane;
    }

    public void Bounce()
    {
        rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
    }
}
