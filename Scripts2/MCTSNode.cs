using System.Collections.Generic;
using UnityEngine;

public class MCTSNode
{
    public MCTSNode Parent;
    public List<MCTSNode> Children;
    public float Wins;
    public float Visits;
    public GameState State;

    public MCTSNode(GameState state, MCTSNode parent = null)
    {
        State = state;
        Parent = parent;
        Children = new List<MCTSNode>();
    }

    public void Expand()
    {
        List<GameState> possibleStates = State.GetPossibleStates();
        foreach (GameState state in possibleStates)
        {
            MCTSNode childNode = new MCTSNode(state, this);
            Children.Add(childNode);
        }
    }

    public MCTSNode BestChild()
    {
        MCTSNode bestChild = null;
        float bestValue = float.MinValue;
        foreach (MCTSNode child in Children)
        {
            float uctValue = child.Wins / child.Visits + Mathf.Sqrt(2 * Mathf.Log(Visits) / child.Visits);
            if (uctValue > bestValue)
            {
                bestValue = uctValue;
                bestChild = child;
            }
        }
        return bestChild;
    }

    public float Simulate()
    {
        GameState currentState = State;
        for (int i = 0; i < 10; i++) // Adjust the number of simulations
        {
            if (currentState.IsTerminal())
            {
                break;
            }

            List<GameState> possibleStates = currentState.GetPossibleStates();
            if (possibleStates.Count == 0)
            {
                break;
            }

            currentState = possibleStates[Random.Range(0, possibleStates.Count)];
        }

        return currentState.EvaluateState();
    }

    public void Backpropagate(float result)
    {
        Visits++;
        Wins += result;
        Parent?.Backpropagate(result);
    }
}










