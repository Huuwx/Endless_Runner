using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private PlayerMovement playerMovement;

    public Animator animator;

    private bool isAlive = true;
    public void SetIsAlive(bool isAlive) { this.isAlive = isAlive; }
    public bool GetIsAlive() { return isAlive; }

    private bool isGrounded = false;
    public void SetIsGrounded(bool isGrounded) {  this.isGrounded = isGrounded;}
    public bool GetIsGrounded() {  return isGrounded; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        if(transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("Death");
        GameOver();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            animator.SetTrigger("Attack");
            collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
            collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
        }
        else if (collision.gameObject.CompareTag("Bridge"))
        {
            playerMovement.BackToCurrentLane();
        }
        else if (collision.gameObject.CompareTag("Bounce"))
        {
            Animator Banimator = collision.gameObject.GetComponent<Animator>();
            Banimator.SetTrigger("Activate");
            playerMovement.Bounce();
            animator.SetBool("Jump", true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
