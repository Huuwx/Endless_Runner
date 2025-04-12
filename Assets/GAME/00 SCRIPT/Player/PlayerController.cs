using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public Animator animator;
    
    public PlayerParameters playerParameters = new PlayerParameters();
    
    public bool isGrounded = false;

    private void Awake()
    {
        isGrounded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.y < -5)
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        playerParameters.State = PlayerState.Dead;
        if (playerMovement.GetIsZPositive())
        {
            playerMovement.rb.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
        }
        else
        {
            playerMovement.rb.AddForce(new Vector3(-1, 1, 0) * 5, ForceMode.Impulse);
        }
        playerMovement.rb.excludeLayers |= (1 << LayerMask.NameToLayer("Barrier"));
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.death);
        animator.SetTrigger("Death");
        GameOver();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerParameters.IsAlive)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {

                //SoundController.Instance.PlayOneShot(SoundController.Instance.jump_land);

                animator.SetBool("Jump", false);
                isGrounded = true;
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                playerMovement.ResetCollider();
                GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.atk_Sword);
                animator.SetTrigger("Attack");
                collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
                collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
                GameManager.Instance.ItemManager.ChangeItem(0);
                ItemController.Instance.ItemtUseTime();
            }
            else if (collision.gameObject.CompareTag("Bridge"))
            {
                playerMovement.BackToOldLane();
            }
            else if (collision.gameObject.CompareTag("Bounce"))
            {
                GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.bound);
                Animator Banimator = collision.gameObject.GetComponent<Animator>();
                Banimator.SetTrigger("Activate");
                playerMovement.Bounce();
                animator.SetBool("Jump", true);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerParameters.IsAlive)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (playerParameters.IsAlive)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerParameters.IsAlive)
        {
            if (other.gameObject.CompareTag("Shield"))
            {
                GameManager.Instance.ItemManager.ChangeItem(2);
                ItemController.Instance.ItemtUseTime();
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("X2"))
            {
                GameManager.Instance.ItemManager.ChangeItem(1);
                Destroy(other.gameObject);
            }
        }
    }
}
