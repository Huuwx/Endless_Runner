﻿using System.Collections;
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

    private void Awake()
    {
        center = 0;
        isZPositive = true;
        canTurn = false;
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
        if (PlayerParameters.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            CheckTurn();

            if (SwipeManager.swipeUp && PlayerParameters.Instance.GetIsGrounded() == true)
            {
                ResetCollider();
                Jump();
            }

            if (SwipeManager.swipeLeft && !canTurn)
            {
                if (PlayerParameters.Instance.GetIsGrounded())
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
            else if (SwipeManager.swipeRight && !canTurn)
            {
                if (PlayerParameters.Instance.GetIsGrounded())
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
        if (hitColliders.Length != 0)
        {
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
        else
        {
            canTurn = false;
        }
    }

    private void Jump()
    {
        SoundController.Instance.PlayOneShot(SoundController.Instance.jump);
        PlayerController.Instance.animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * PlayerParameters.Instance.GetJumpForce(), ForceMode.Impulse);
    }

    private IEnumerator Slide()
    {
        if(!PlayerParameters.Instance.GetIsGrounded())
        {
            rb.AddForce(Vector3.down * PlayerParameters.Instance.GetJumpForce(), ForceMode.Impulse);
        }
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

    public bool GetIsZPositive()
    {
        return isZPositive;
    }
}
