using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public Animator animator;
    
    public PlayerParameters playerParameters = new PlayerParameters();
    
    Collider playerCollider;
    
    public bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        isGrounded = false;
        playerCollider = GetComponent<Collider>();
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
        CheckGrounded();
        Vector3 origin = transform.position + Vector3.down * 0.5f;
        //if(transform.position.y < -5)
        //{
        //    Die();
        //}
    }

    public void CheckGrounded()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundLayer);
        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
            return;
        }

        Debug.Log(hit.transform.name);
        animator.SetBool("Jump", false);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0 , 0.5f), new Vector3(0, -0.2f, 0), Color.red);
        Debug.DrawRay(transform.position, new Vector3(0, -0.2f, 0), Color.red);
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
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                playerMovement.ResetCollider();
                GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.atk_Sword);
                animator.SetTrigger("Attack");
                collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
                collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
                GameManager.Instance.ItemManager.ChangeItem(collision.gameObject.GetComponent<ItemIndex>().index);
                GameManager.Instance.ItemController.ItemUseTime();
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
        // if (playerParameters.IsAlive)
        // {
        //     if (collision.gameObject.CompareTag("Ground"))
        //     {
        //         isGrounded = true;
        //     }
        // }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if (playerParameters.IsAlive)
        // {
        //     if (collision.gameObject.CompareTag("Ground"))
        //     {
        //         isGrounded = false;
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerParameters.IsAlive)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                GameManager.Instance.ItemManager.ChangeItem(other.gameObject.GetComponent<ItemIndex>().index);
                GameManager.Instance.ItemController.ItemUseTime();
                other.gameObject.SetActive(false);
            }
        }
    }
}
