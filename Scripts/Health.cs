using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 
    public TextMeshProUGUI healthText; 
    private string playerName;

    private void Start()
    {
       
        currentHealth = maxHealth;
        playerName = gameObject.name;
        UpdateHealthUI();
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();        }
    }

    public void Heal(int healAmount)
    {

        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthUI();
    }

    // Method to handle player death
    private void Die()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player1 != null && player1.GetComponent<Health>().currentHealth <= 0)
        {
            SceneManager.LoadScene("Player2Win");
        }
        else if (player2 != null && player2.GetComponent<Health>().currentHealth <= 0)
        {
            SceneManager.LoadScene("Player1Win");
        }
    }

    // Method to update the UI text element with the current health
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = playerName + " Health: " + currentHealth.ToString();
        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
      //  Debug.Log("Health increased by " + amount + ". Current health: " + currentHealth);
        UpdateHealthUI();
    }
}
