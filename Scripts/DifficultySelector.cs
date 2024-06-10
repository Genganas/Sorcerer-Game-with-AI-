using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public GameObject difficultySelectionPanel;

    
    public void SetEasyDifficulty()
    {
        SetDifficulty(5); // Easy difficulty with 5 iterations
    }

  
    public void SetMediumDifficulty()
    {
        SetDifficulty(18); // Medium difficulty with 18 iterations
    }

    
    public void SetHardDifficulty()
    {
        SetDifficulty(30); // Hard difficulty with 30 iterations
    }

    private void SetDifficulty(int iterations)
    {
        NewP2Controller.MCTSIterations = iterations;
        Invoke("DisablePanel", 1f);
    }

    private void DisablePanel()
    {
        difficultySelectionPanel.SetActive(false);
    }
}
