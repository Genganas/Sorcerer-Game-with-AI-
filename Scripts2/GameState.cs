using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Vector3 Player1Pos { get; private set; }
    public Vector3 Player2Pos { get; private set; }
    public int Player1Health { get; private set; }
    public int Player2Health { get; private set; }
    public bool IsPlayer1Turn { get; private set; }


    private const float AttackRange = 5f;
    private const int AttackDamage = 10;
    private const int DefendBenefit = 5;
    private const float MaxMoveDistance = 4f;
    private const float PowerUpBonus = 20f; 
    private const float PositionalWeight = 10f; 
    private const float TurnAdvantage = 5f; 

    public GameState(Vector3 player1Pos, Vector3 player2Pos, int player1Health, int player2Health, bool isPlayer1Turn)
    {
        Player1Pos = player1Pos;
        Player2Pos = player2Pos;
        Player1Health = player1Health;
        Player2Health = player2Health;
        IsPlayer1Turn = isPlayer1Turn;
    }

    public List<GameState> GetPossibleStates()
    {
        List<GameState> possibleStates = new List<GameState>();

        if (IsPlayer1Turn)
        {
            possibleStates.AddRange(GetPossibleMoves(Player1Pos, Player1Health, Player2Pos, Player2Health, false));
        }
        else
        {
            possibleStates.AddRange(GetPossibleMoves(Player2Pos, Player2Health, Player1Pos, Player1Health, true));
        }

        return possibleStates;
    }

    private List<GameState> GetPossibleMoves(Vector3 currentPlayerPos, int currentPlayerHealth, Vector3 opponentPos, int opponentHealth, bool nextTurnIsPlayer1)
    {
        List<GameState> moves = new List<GameState>();

        // Move logic with positional advantage and power-up status
        Vector3 nearestTile = FindNearestValidTile(currentPlayerPos, opponentPos);
        float positionalAdvantage = CalculatePositionalAdvantage(currentPlayerPos);
        float powerUpStatus = CheckPowerUpStatus(currentPlayerPos);
        if (nearestTile != Vector3.zero)
        {
            // Adjust utility calculation by adding positional advantage and power-up bonus
            float utility = EvaluateState() + PositionalWeight * positionalAdvantage + PowerUpBonus * powerUpStatus;
            moves.Add(new GameState(nextTurnIsPlayer1 ? opponentPos : nearestTile, nextTurnIsPlayer1 ? nearestTile : opponentPos, currentPlayerHealth, opponentHealth, nextTurnIsPlayer1));
        }

        // Attack logic
        if (Vector3.Distance(currentPlayerPos, opponentPos) <= AttackRange)
        {
            int newOpponentHealth = opponentHealth - AttackDamage;
            moves.Add(new GameState(currentPlayerPos, opponentPos, currentPlayerHealth, newOpponentHealth, nextTurnIsPlayer1));
        }

        // Defend logic
        int newPlayerHealth = currentPlayerHealth + DefendBenefit;
        moves.Add(new GameState(currentPlayerPos, opponentPos, newPlayerHealth, opponentHealth, nextTurnIsPlayer1));

        return moves;
    }

    private bool IsValidPosition(Vector3 pos)
    {
        
        return true;
    }

    private Vector3 FindNearestValidTile(Vector3 currentPosition, Vector3 targetPosition)
    {
        Vector3 nearestTilePosition = Vector3.zero;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            float distanceToTile = Vector3.Distance(currentPosition, tile.transform.position);
            float distanceToTarget = Vector3.Distance(tile.transform.position, targetPosition);

            if (distanceToTile <= MaxMoveDistance && distanceToTarget < nearestDistance && distanceToTarget > 0.1f && IsValidPosition(tile.transform.position))
            {
                nearestDistance = distanceToTarget;
                nearestTilePosition = tile.transform.position;
            }
        }

        return nearestTilePosition;
    }

    public bool IsTerminal()
    {
        return Player1Health <= 0 || Player2Health <= 0;
    }

    public float EvaluateState()
    {
        // Adjusted utility function considering health, positional advantage, power-up status, and turn advantage
        return Player1Health - Player2Health + TurnAdvantage * (IsPlayer1Turn ? 1 : -1);
    }

    private float CalculatePositionalAdvantage(Vector3 playerPosition)
    {
      
        return 0; // Replace with actual calculation
    }

    private float CheckPowerUpStatus(Vector3 playerPosition)
    {
    
        return 0; // Replace with actual calculation
    }
}