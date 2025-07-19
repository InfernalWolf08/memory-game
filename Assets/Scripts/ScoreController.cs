using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Score")]
    public int score;
    private int points;
    [HideInInspector] public int highScore;
    public string difficulty; // Used for settings different highscores per difficulty: PlayerPrefs.GetInt(difficulty + " highscore", 0)
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI addedScore;
    private PlayerDraw drawer;
    [Space]
    public float scoreSpeed=1f;

    void Start()
    {
        // Initialize
        currentScore.text = "Score: 0";
        addedScore.text = "";
        drawer = GetComponent<PlayerDraw>();
        if (!PlayerPrefs.HasKey(difficulty + " highscore"))
        {
            PlayerPrefs.SetInt(difficulty + " highscore", 0);
        }
    }

    void Update()
    {
        // Update text
        addedScore.text = Convert.ToString(points);
        currentScore.text = $"Score: {score}";
    }

    // Custom Functions
    public void setHighscore()
    {
        highScore = PlayerPrefs.GetInt(difficulty + " highscore");

        if (highScore < score)
        {
            PlayerPrefs.SetInt(difficulty + " highscore", score);
            highScore = score;
        }
    }

    // Coroutines
    public IEnumerator displayScore(Color32[] player, Color32[] image, GameObject[] playerBoard, GameObject[] gameBoard)
    {
        // Initialize
        int scoreTemp = score;
        int pointsTemp = 0;
        addedScore.text = "0";

        // Get points
        for (int i=0; i<image.Length; i++)
        {
            // Display a little square thingy moving across the board
            int darkenValue = 5;
            print(i);
            SpriteRenderer playerTile = playerBoard[i].GetComponent<SpriteRenderer>();
            SpriteRenderer imageTile = gameBoard[i].GetComponent<SpriteRenderer>();
            
            Color32 ogPlayerColor = playerTile.color;
            Color32 ogImageColor = imageTile.color;

            playerTile.color = new Color32(Convert.ToByte(playerTile.color.r+5), Convert.ToByte(playerTile.color.g+5), Convert.ToByte(playerTile.color.b+5), 255);
            imageTile.color = new Color32(Convert.ToByte(imageTile.color.r+5), Convert.ToByte(imageTile.color.g+5), Convert.ToByte(imageTile.color.b+5), 255);

            yield return new WaitForSeconds(scoreSpeed*.025f);

            playerTile.color = ogPlayerColor;
            imageTile.color = ogImageColor;

            if (player[i].Equals(image[i]))// /*skips white tiles ->*/ && (!player[i].Equals(Color.white) && !image[i].Equals(Color.white)))
            {
                // Display points
                yield return new WaitForSeconds(scoreSpeed*.075f);
                points += 1;
                pointsTemp += 1;
            }
        }

        yield return new WaitForSeconds(1f);

        // Update score
        while (score+1<=scoreTemp+pointsTemp)
        {
            // Display score
            yield return new WaitForSeconds(scoreSpeed*.025f);
            score += 1;
            points -= 1;
            
            yield return null;
        }

        // Finish
        StartCoroutine(drawer.reset());
        yield return null;
    }
}