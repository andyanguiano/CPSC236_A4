using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 25f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;

    public AudioClip jumpClip, gameOverClip;

    public GameObject gameOverText, winText;

    private void Start()
    {
        gameOverText.SetActive(false);
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(animator.GetBool("IsJumping") == false)
            {
                jump = true;
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            }
            
        }

    }

    public void onLanding()
    {
        jump = false;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("BARREL"))
        {
            Time.timeScale = 0f;
            gameOverText.SetActive(true);
            AudioSource.PlayClipAtPoint(gameOverClip, transform.position);
        }
        if (col.gameObject.tag.Equals("Player"))
        {
            Time.timeScale = 0f;
            winText.SetActive(true);
        }
    }

}
