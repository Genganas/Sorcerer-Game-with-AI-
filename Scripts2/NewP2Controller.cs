using UnityEngine;

public class NewP2Controller : MonoBehaviour
{
    private MCTS mcts;
    public PlayerMovement1 player1;
    public PlayerMovement2 player2;
    public static int MCTSIterations = 10; // Default to Medium difficulty

    void Start()
    {
        InitializeMCTS();
        Debug.Log("the amount of iterations: " + MCTSIterations);
    }

    void Update()
    {
        if (!GameManagerNEW.Instance.IsPlayer1Turn())
        {
            InitializeMCTS();
            RunMCTS();
        }

        Debug.Log("the amount of iterations: " + MCTSIterations);
    }

    private void InitializeMCTS()
    {
        Vector3 player1Pos = player1.transform.position;
        Vector3 player2Pos = player2.transform.position;
        int player1Health = player1.health.currentHealth;
        int player2Health = player2.health.currentHealth;
        bool isPlayer1Turn = GameManagerNEW.Instance.IsPlayer1Turn();

        GameState initialState = new GameState(player1Pos, player2Pos, player1Health, player2Health, isPlayer1Turn);
        mcts = new MCTS(initialState);
    }

    public void RunMCTS()
    {
        // Run the MCTS search
        mcts.RunSearch(MCTSIterations); // Adjust the number of iterations as needed

        // Get the best move from the MCTS search
        GameState bestMove = mcts.GetBestMove();

        // Randomly select an action: Move, Attack, or Defend
        float randomAction = Random.Range(0f, 1f);
        if (randomAction < 0.33f)
        {
            // Move closer
            MoveCloserToPlayer(bestMove);
        }
        else if (randomAction < 0.66f)
        {
            // Attack
            AttackOpponent(bestMove);
        }
        else
        {
            // Defend
            Defend(bestMove);
        }

        // End the AI's turn
        GameManagerNEW.Instance.EndPlayerTurn();
    }

    private void MoveCloserToPlayer(GameState gameState)
    {
        // Move closer to the opponent
        player2.transform.position = gameState.Player2Pos;
        player2.health.currentHealth = gameState.Player2Health;
    }

    private void AttackOpponent(GameState gameState)
    {
        // Attack the opponent
        player2.transform.position = gameState.Player2Pos;
        player2.health.currentHealth = gameState.Player2Health;
        player2.Attack();
    }

    private void Defend(GameState gameState)
    {
        // Defend
        player2.transform.position = gameState.Player2Pos;
        player2.health.currentHealth = gameState.Player2Health;
        player2.Defend();
    }
}




















