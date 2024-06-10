using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public PlayerMovement1 player1Controller;
    public float moveSpeed = 5f;
    public LayerMask tileLayer;
    public float maxMoveDistance = 1.1f; 
    public float attackRange = 3f; 
    public int attackDamage = 10;
    bool selectedPlayer = false;
    [SerializeField] Transform startingPosition;
    private bool isMoving = false;
    public GameObject fireballPrefab;
    public float fireballSpeed;
    [SerializeField] GameObject FireBallSpawnPoint;
    public Animator Character2;

    public Health health;
    public int defenseBonus;

    public TextMeshProUGUI notyourturnText;

    private void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Character2.SetBool("IsMoving", true);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                Vector3 targetPosition = hit.collider.transform.position;

                if (IsAdjacent(transform.position, targetPosition))
                {
                    StartCoroutine(MoveToTile(targetPosition));
                    FindObjectOfType<GameManagerNEW>().EndPlayerTurn();
                }
            }
        }

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Character2.SetBool("IsMoving", false);
    }

    private bool IsAdjacent(Vector3 currentPosition, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(currentPosition, targetPosition);
        return distance <= maxMoveDistance;
    }

    private IEnumerator MoveToTile(Vector3 targetPosition)
    {
        isMoving = true;

        if (Character2 != null)
        {
            Character2.SetBool("IsRunning", true);
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Rotate towards the target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;

        if (Character2 != null)
        {
            Character2.SetBool("IsRunning", false);
        }
    }

    public void Attack()
    {
        if (!FindObjectOfType<GameManagerNEW>().IsPlayer2Turn())
        {
            StartCoroutine(DisplayAndFade(notyourturnText, "Player 2 it's not your turn."));
            return;
        }

        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        float distanceToOpponent = Vector3.Distance(transform.position, player1Controller.transform.position);
        if (distanceToOpponent <= attackRange)
        {
            // Rotate the character to face the opponent
            Vector3 directionToOpponent = (player1Controller.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToOpponent.x, 0, directionToOpponent.z));

            float rotationSpeed = 10f;
            while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                yield return null;
            }

            Character2.SetBool("IsAttacking", true);
            Character2.SetBool("IsMoving", false);
            Character2.SetBool("AreaAttack", false);

            Health opponentHealth = player1Controller.GetComponent<Health>();

            if (opponentHealth != null)
            {
                GameObject fireball = Instantiate(fireballPrefab, FireBallSpawnPoint.transform.position, Quaternion.identity);
                Vector3 direction = (player1Controller.transform.position - transform.position).normalized;
                fireball.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;

                opponentHealth.TakeDamage(attackDamage);

                FindObjectOfType<GameManagerNEW>().EndPlayerTurn();

                StartCoroutine(ResetAttackAnimation());
            }
            else
            {
                Debug.Log("Opponent's health script not found.");
            }
        }
        else
        {
            Debug.Log("Cannot reach opponent.");
        }
    }

    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        Character2.SetBool("IsAttacking", false);
    }

    public void SetSelectedPlayer(bool isSelected)
    {
        selectedPlayer = isSelected;
    }

    public void RespawnToStartingPosition()
    {
        transform.position = startingPosition.position;
    }

    public void TakeDamage(int damage)
    {
        damage = 10;
        health.TakeDamage(damage);
    }

    public void Defend()
    {
        if (!FindObjectOfType<GameManagerNEW>().IsPlayer2Turn())
        {
            notyourturnText.text = "Player 2 it's not your turn.";
            return;
        }

        health.Heal(defenseBonus);

        Debug.Log("Player 2 defends and gains health.");
        notyourturnText.text = "Player 2 defends and gains health";
        FindObjectOfType<GameManagerNEW>().EndPlayerTurnAfterDefense();
    }

    public void DoubleFireballDistance()
    {
        attackRange *= 2;
    }

    public void ResetFireballDistance()
    {
        attackRange /= 2;
    }

    public void DoubleFireballDamageNextTurn()
    {
        attackDamage *= 2;
    }

    public void ResetFireballDamageNextTurn()
    {
        attackDamage /= 2;
    }

    private IEnumerator DisplayAndFade(TextMeshProUGUI textMeshPro, string text)
    {
        textMeshPro.text = text;
        Color originalColor = textMeshPro.color;
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(2f);

        float fadeDuration = 1.5f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

}