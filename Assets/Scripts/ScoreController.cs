using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Score")]
    public int score;
    private int points;
    public int highScore;
    public string difficulty;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI addedScore;

    void Start()
    {
        // Initialize
        currentScore.text = "Score: 0";
        addedScore.text = "";
    }

    void Update()
    {
        // Update text
        addedScore.text = Convert.ToString(points);
        currentScore.text = $"Score: {score}";
    }

    // Custom Functions
    

    // Coroutines
    public IEnumerator displayScore(Color32[] player, Color32[] image)
    {
        // Initialize
        int scoreTemp = score;
        int pointsTemp = 0;
        addedScore.text = "0";

        // Get points
        for (int i=0; i<image.Length; i++)
        {
            if (player[i].Equals(image[i]))// /*white tiles don't give points ->*/ && (!player[i].Equals(Color.white) && !image[i].Equals(Color.white)))
            {
                // Display points
                yield return new WaitForSeconds(.1f);
                points += 1;
                pointsTemp += 1;
            }
        }

        yield return new WaitForSeconds(1f);

        // Update score
        while (score+1<=scoreTemp+pointsTemp)
        {
            // Display score
            yield return new WaitForSeconds(.025f);
            score += 1;
            points -= 1;
            
            yield return null;
        }

        yield return null;
    }
}