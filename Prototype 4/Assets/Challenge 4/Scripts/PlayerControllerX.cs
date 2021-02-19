using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private float boostModificator = 20.0f;
    private float boostDuration = 7.0f;
    private GameObject focalPoint;
    private IEnumerator coroutine;

    public bool hasPowerup;
    public bool isBoosting;
    public GameObject powerupIndicator;
    public ParticleSystem boostParticles;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting)
        {
            isBoosting = true;
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * boostModificator, ForceMode.Impulse);
            boostParticles.Play();
            startBoostCooldownCoroutine();
        }

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            startPowerupCooldownCoroutine();
        }
    }

    // Coroutine to count down powerup duration
    public IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    public void startPowerupCooldownCoroutine()
    {
        coroutine = PowerupCooldown();
        StartCoroutine(coroutine);
    }

    public void stopPowerupCooldownCoroutine()
    {
        StopCoroutine("PowerupCooldown");
    }

    public IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(boostDuration);
        isBoosting = false;
    }

    public void startBoostCooldownCoroutine()
    {
        coroutine = BoostCooldown();
        StartCoroutine(coroutine);
    }

    public void stopBoostCooldownCoroutine()
    {
        isBoosting = false;
        boostParticles.Stop();
        StopCoroutine("BoostCooldown");
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
