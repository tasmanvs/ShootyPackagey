using UnityEngine;
using TMPro; // Add this namespace

public class ScoreTracker : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI instead of Text

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
}
