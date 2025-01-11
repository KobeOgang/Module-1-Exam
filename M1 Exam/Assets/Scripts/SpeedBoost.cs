using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 10f;       
    public float boostDuration = 3f;      

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.boost);
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
        }
    }

    private System.Collections.IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalMaxSpeed = playerMovement.maxSpeed;
        float originalAcceleration = playerMovement.acceleration;

        playerMovement.maxSpeed += boostAmount;
        playerMovement.acceleration += boostAmount / 2f; 

        yield return new WaitForSeconds(boostDuration);

        playerMovement.maxSpeed = originalMaxSpeed;
        playerMovement.acceleration = originalAcceleration;
    }
}
