using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider boxCollider;
    public Rigidbody rb;

    private int oldLane = 1;
    private int desiredLane = 1;

    private float center = 0;
    private float centerZ = -20;
    private float laneDistance = 3;

    [SerializeField] LayerMask turnLayer;

    private bool isZPositive = true;
    private bool canTurn = false;

    //[SerializeField] float speed = 0f;


    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = rb.GetComponent<BoxCollider>();
        center = 0;
        isZPositive = true;
        canTurn = false;
    }
    
    // private void Awake()
    // {
    //     center = 0;
    //     isZPositive = true;
    //     canTurn = false;
    // }

    // Start is called before the first frame update
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     boxCollider = rb.GetComponent<BoxCollider>();
    // }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Player.playerParameters.IsAlive == true && GameManager.Instance.isStarted)
        {
            CheckTurn();

            if (SwipeManager.swipeUp && GameManager.Instance.Player.isGrounded == true)
            {
                ResetCollider();
                Jump();
            }

            if (SwipeManager.swipeLeft && !canTurn)
            {
                if (GameManager.Instance.Player.isGrounded)
                {
                    ResetCollider();
                    GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.dash);
                    GameManager.Instance.Player.animator.SetTrigger(CONSTANT.SwipeLeft);
                }
                desiredLane--;
                oldLane = desiredLane + 1;
                if (desiredLane == -1)
                {
                    desiredLane = 0;
                    oldLane = 0;
                }
            }
            else if (SwipeManager.swipeRight && !canTurn)
            {
                if (GameManager.Instance.Player.isGrounded)
                {
                    ResetCollider();
                    GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.dash);
                    GameManager.Instance.Player.animator.SetTrigger(CONSTANT.SwipeRight);
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

                    targetPos += Vector3.left * laneDistance;
                }
                else if (desiredLane == 2)
                {
                    targetPos += Vector3.right * laneDistance;
                }
            }
            else
            {
                targetPos = new Vector3(transform.position.x, transform.position.y, centerZ);

                if (desiredLane == 0)
                {
                    targetPos += Vector3.forward * laneDistance;

                }
                else if (desiredLane == 2)
                {

                    targetPos += Vector3.back * laneDistance;
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
        //Vector3 forwardMove = Vector3.forward * speed * Time.deltaTime;
        //rb.MovePosition(rb.position + forwardMove);
    }

    private void CheckTurn()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, turnLayer);
        if (hitColliders.Length == 0)
        {
            canTurn = false;
            return;
        }
        
        canTurn = true;
        TurnGroundController turnGroundController = hitColliders[0].GetComponent<TurnGroundController>();
        if (SwipeManager.swipeRight && canTurn && (turnGroundController.turnDir == TurnGroundController.TURNDIR.right))
        {
            isZPositive = false;
            transform.position = turnGroundController.pivot.position;
            centerZ = turnGroundController.pivot.position.z;
            turnGroundController.spawner.moveDirection = new Vector3(-1, 0, 0);
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            desiredLane = 1;
            oldLane = 1;
        }
        else if (SwipeManager.swipeLeft && canTurn && (turnGroundController.turnDir == TurnGroundController.TURNDIR.left))
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

    private void Jump()
    {
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.jump);
        GameManager.Instance.Player.animator.SetBool(CONSTANT.Jump, true);
        rb.AddForce(Vector3.up * GameManager.Instance.Player.playerParameters.GetJumpForce(), ForceMode.Impulse);
    }

    private IEnumerator Slide()
    {
        if(!GameManager.Instance.Player.isGrounded)
        {
            rb.AddForce(Vector3.down * GameManager.Instance.Player.playerParameters.GetJumpForce(), ForceMode.Impulse);
        }
        boxCollider.size = new Vector3(1, 0.5f, 1);
        boxCollider.center = new Vector3(0, 0.25f, 0);
        GameManager.Instance.Player.animator.SetBool(CONSTANT.Sliding, true);

        yield return new WaitForSeconds(1f);

        ResetCollider();
    }

    public void ResetCollider()
    {
        boxCollider.size = new Vector3(1, 2.3f, 1);
        boxCollider.center = new Vector3(0, 1.1f, 0);
        GameManager.Instance.Player.animator.SetBool(CONSTANT.Sliding, false);
    }

    public void BackToOldLane()
    {
        desiredLane = oldLane;
    }

    public void Bounce()
    {
        rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
    }

    public bool GetIsZPositive()
    {
        return isZPositive;
    }
}
