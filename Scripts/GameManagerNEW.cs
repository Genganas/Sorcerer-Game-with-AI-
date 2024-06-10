using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManagerNEW : MonoBehaviour
{
    public PlayerMovement1 player1Controller;
    public PlayerMovement2 player2Controller;

    private bool isPlayer1Turn = true;
    private bool isPlayer2Turn = false;
    public TextMeshProUGUI playerturn;

    [SerializeField] GameObject playerOneTurn;
    [SerializeField] GameObject playerTwoTurn;
    private int turnCount = 0;

    [SerializeField] bool AIisPlaying;

    public static GameManagerNEW Instance { get; private set; }
    void Start()
    {
        // Start the game with Player 1's turn
        StartPlayerTurn();
        turnCount = 0;
       // Debug.Log(turnCount);
        Instance = this;
    }
    
    public void StartPlayerTurn()
    {
        // Enable the appropriate player controller and disable the other
        if (isPlayer1Turn)
        {
            player1Controller.enabled = true;
            player2Controller.enabled = false;
            player1Controller.SetSelectedPlayer(true); 
            player2Controller.SetSelectedPlayer(false); 
          //  Debug.Log("Player 1's turn");
            playerOneTurn.SetActive(true);
            playerTwoTurn.SetActive(false);
            turnCount++;
        }
        else if (isPlayer2Turn)
        {
            player1Controller.enabled = false;
            player2Controller.enabled = true;
            player1Controller.SetSelectedPlayer(false);
            player2Controller.SetSelectedPlayer(true);
            
            playerTwoTurn.SetActive(true);
            playerOneTurn.SetActive(false); 
            turnCount++;
        }
        Debug.Log(GetTurnCount());
      
        if (turnCount >= 3)
        {
            ResetTurnCount();
        }

    }

  
    public void ResetTurnCount()
    {
        turnCount = -1; 
      //  Debug.Log("Resetting the tile");
    }

    // Method to get the current turn count
    public int GetTurnCount()
    {
        return turnCount;
    }

    public void RespawnPlayers()
    {
        player1Controller.RespawnToStartingPosition();
        player2Controller.RespawnToStartingPosition();
    }

    public void EndPlayerTurn()
    {
        if (isPlayer1Turn)
        {
            isPlayer1Turn = false;
            isPlayer2Turn = true;
        }
        else if (isPlayer2Turn)
        {
            isPlayer2Turn = false;
            isPlayer1Turn = true;
        }
        StartPlayerTurn();
    }

    public bool IsPlayer1Turn()
    {
        return isPlayer1Turn;
    }

    public bool IsPlayer2Turn()
    {
        return isPlayer2Turn;
    }

    public void EndPlayerTurnAfterDefense()
    {
        if (isPlayer1Turn)
        {
            isPlayer1Turn = false;
            isPlayer2Turn = true;
        }
        else if (isPlayer2Turn)
        {
            isPlayer2Turn = false;
            isPlayer1Turn = true;
        }
        StartPlayerTurn();
    }

    public void DealDamageToPlayer2(int damage)
    {
        // Deal damage to player 2
        player2Controller.TakeDamage(damage);
    }
    public void DealDamageToPlayer1(int damage)
    {
        // Deal damage to player 2
        player1Controller.TakeDamage(damage);
    }

}