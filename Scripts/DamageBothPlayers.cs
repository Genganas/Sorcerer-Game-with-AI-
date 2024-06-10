using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageBothPlayers : MonoBehaviour
{
    private bool powerUpTriggered = false;
    private Collider powerUpCollider;
    private bool player1Damaged = false;
    private bool player2Damaged = false;
    [SerializeField] AudioSource magicSound;
    [SerializeField] GameObject rockHit;

    private bool reset = false;

    private void Start()
    {
        // Get reference to the collider component
        powerUpCollider = GetComponent<Collider>();
        rockHit.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a player and the power-up logic hasn't been triggered yet
        if (!powerUpTriggered && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            rockHit.SetActive(true);
            magicSound.Play();
            // Deal 5 damage to both players if they haven't been damaged before
            if (other.CompareTag("Player1") && !player1Damaged)
            {
                TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
                player2powerup.text = "Earthquake spell Players -15 health.";
                player2powerup.color = Color.cyan;
                TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
                player1powerup.text = "Earthquake spell Players -15 health.";
                player1powerup.color = Color.cyan;
                GameManagerNEW.Instance.DealDamageToPlayer1(15);
                player1Damaged = true;
                GameManagerNEW.Instance.DealDamageToPlayer2(15);
                player2Damaged = true;

               
            }
            if (other.CompareTag("Player2") && !player2Damaged)
            {
                magicSound.Play();
                TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
                player2powerup.text = "Earthquake spell Players -15 health.";
                TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
                player1powerup.text = "Earthquake spell Players -15 health.";
                GameManagerNEW.Instance.DealDamageToPlayer1(15);
                player1Damaged = true;
                GameManagerNEW.Instance.DealDamageToPlayer2(15);
                player2Damaged = true;

                
            }

        
            powerUpTriggered = true;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        magicSound.Stop();
        if (other.CompareTag("Player1") && player1Damaged)
        {
            player1Damaged = false;
            player2Damaged = false;

            powerUpTriggered = false;
            DisableTile();
        }
        else
        if (other.CompareTag("Player2") && player2Damaged)
        {
            
            player1Damaged = false;
            player2Damaged = false;

            powerUpTriggered = false;
            DisableTile();
        }
    }

   
    private new Renderer renderer;
    public Material originalMaterial;
    public Material greyMaterial;
    private void DisableTile()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = greyMaterial;
       
        powerUpCollider.enabled = false;
        reset = true;
        rockHit.SetActive(false);
    }

    public void EnableTile()
    {
        if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
        {
            renderer.material = originalMaterial;
            powerUpCollider.enabled = true;
        }
    }
    private void Update()
    {
        if (reset)
        {
            if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
            {
                renderer.material = originalMaterial;
                Collider collider = GetComponent<Collider>();
                collider.enabled = true;
                reset = false;
            }

        }
    }
}