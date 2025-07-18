using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Score")]
    public int score;
    public int highScore;
    public string difficulty;

    // Custom Functions
    public void compare(Color32[] player, Color32[] image)
    {
        for (int i=0; i<image.Length; i++)
        {
            if (player[i].Equals(image[i]))
            {
                score += 1;
            }
        }
    }
}