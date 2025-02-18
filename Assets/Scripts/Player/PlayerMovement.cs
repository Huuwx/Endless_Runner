using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    BoxCollider boxCollider;
    private Rigidbody rb;

    private int currentLane = 1;
    private int desiredLane = 1;
    public float laneDistance = 3;

    [SerializeField] float speed = 0f;
    //Jump
    [SerializeField] float jumpForce = 100f;

    [SerializeField] List<GameObject> Lane;

    [SerializeField] LayerMask turnLayer;


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

            Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
            rb.MovePosition(rb.position + forwardMove);

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
                currentLane = desiredLane + 1;
                if (desiredLane == -1)
                {
                    desiredLane = 0;
                    currentLane = 0;
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
                currentLane = desiredLane - 1;
                if (desiredLane == 3)
                {
                    desiredLane = 2;
                    currentLane = 2;
                }
            }

            if (SwipeManager.swipeDown)
            {
                StartCoroutine(Slide());
            }

            Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

            Debug.Log(targetPos);

            if (desiredLane == 0)
            {
                targetPos += Vector3.left * laneDistance;
            }
            else if (desiredLane == 2)
            {
                targetPos += Vector3.right * laneDistance;
            }

            //transform.position = Vector3.Lerp(transform.position, targetPos, 80 * Time.deltaTime);

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
        //if (!PlayerController.Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        //{
        //    PlayerController.Instance.animator.SetBool("isSliding", false);
        //    boxCollider.size = new Vector3(1, 2.5f, 1);
        //    boxCollider.center = new Vector3(0, 1.1f, 0);
        //}
    }

    private Vector3? CheckTurn()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, turnLayer);
        if (hitColliders.Length != 0)
        {
            TurnGroundController turnGroundController = hitColliders[0].GetComponent<TurnGroundController>();
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = turnGroundController.pivot.position;
                turnGroundController.spawner.moveDirection = new Vector3(-1, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                desiredLane = 1;
                currentLane = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = turnGroundController.pivot.position;
                turnGroundController.spawner.moveDirection = new Vector3(0, 0, -1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                desiredLane = 1;
                currentLane = 1;
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

    public void BackToCurrentLane()
    {
        desiredLane = currentLane;
    }

    public void Bounce()
    {
        rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
    }
}
