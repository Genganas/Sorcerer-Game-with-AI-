public class MCTS
{
    public MCTSNode Root;

    public MCTS(GameState rootState)
    {
        Root = new MCTSNode(rootState);
    }

    public void RunSearch(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            MCTSNode node = Select(Root);
            float result = node.Simulate();
            node.Backpropagate(result);
        }
    }

    private MCTSNode Select(MCTSNode node)
    {
        while (node.Children.Count > 0)
        {
            node = node.BestChild();
        }
        if (node.Visits > 0)
        {
            node.Expand();
            return node.Children[0];
        }
        return node;
    }

    public GameState GetBestMove()
    {
        MCTSNode bestChild = Root.BestChild();
        return bestChild.State;
    }
}








