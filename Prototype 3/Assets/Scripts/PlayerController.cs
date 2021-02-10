using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float gravityModifier = 1.0f;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatter;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool gameOver;

    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound);
                dirtSplatter.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            dirtSplatter.Play();
        } else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(crashSound);
            explosionParticle.Play();
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
        }
    }
}
