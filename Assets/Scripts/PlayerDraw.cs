using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDraw : MonoBehaviour
{
    [Header("DEBUG")]
    public bool imageEdit;

    [Header("Mouse Data")]
    private Camera cam;
    private CursorController cursor;

    [Header("Painting Data")]
    public Color32 selectedColor = Color.white;
    public List<Color32> canvas = new List<Color32>();
    private bool canDraw;

    [Header("Curtain")]
    public Animator curtain;
    public float curtainWaitTime;

    [Header("Scene")]
    public TileGenerator playerTiles;
    public GameObject finishButton;
    [HideInInspector] public ImageController imageCont;

    [Header("Score")]
    public GameObject endScreen;
    public ScoreController scoreCont;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public TimerController timerCont;

    void Start()
    {
        // Initialize
        cam = GetComponent<Camera>();
        cursor = GetComponent<CursorController>();
        timerCont = GetComponent<TimerController>();
        Cursor.visible = false;
        endScreen.SetActive(false);
        imageCont = UnityEngine.Object.FindFirstObjectByType<ImageController>();
        StartCoroutine(curtainDrop(curtainWaitTime));
    }

    void Update()
    {
        // Hotkeys
        if (Input.GetKeyDown("1") && canDraw)
        {
            selectedColor = new Color32(255, 0, 0, 255); // Red
        } else if (Input.GetKeyDown("2") && canDraw) {
            selectedColor = new Color32(255, Convert.ToByte(127.5), 0, 255); // Orange
        } else if (Input.GetKeyDown("3") && canDraw) {
            selectedColor = new Color32(255, 255, 0, 255); // Yellow
        } else if (Input.GetKeyDown("4") && canDraw) {
            selectedColor = new Color32(0, 255, 0, 255); // Green
        } else if (Input.GetKeyDown("5") && canDraw) {
            selectedColor = new Color32(0, 0, 255, 255); // Blue
        } else if (Input.GetKeyDown("6") && canDraw) {
            selectedColor = new Color32(Convert.ToByte(127.5), 0, 255, 255); // Purple
        } else if (Input.GetKeyDown("7") && canDraw) {
            selectedColor = new Color32(255, 0, 255, 255); // Pink
        } else if (Input.GetKeyDown("8") && canDraw) {
            selectedColor = new Color32(Convert.ToByte(127.5), Convert.ToByte(63.75), 0, 255); // Brown
        } else if (Input.GetKeyDown("9") && canDraw) {
            selectedColor = new Color32(0, 0, 0, 255); // Black
        } else if (Input.GetKeyDown("0") && canDraw) {
            selectedColor = new Color32(255, 255, 255, 255); // White
        }

        // Set cursor color
        cursor.cursor.color = selectedColor;

        // Paint
        if (Input.GetMouseButton(0) && canDraw)
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            
            // Change tile color
            try
            {
                if (hit.transform.gameObject.tag=="Tile")
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = selectedColor;
                }
            } catch (Exception e) {
                print("No tile was hit");
            }
        }

        // Display timer
        if (timerCont.timerActive)
        {
            timerCont.showTimer = canDraw;
        }
    }

    // Custom functions
    public void changeColor(GameObject button)
    {
        selectedColor = button.GetComponent<Image>().color;
        // cursor.setCursorColor(selectedColor);
    }

    public void getDrawing()
    {
        // Get the color of each tile
        finishButton.GetComponent<Animator>().SetBool("isShowing", false);
        GameObject[] painting = playerTiles.tiles;

        // Get the color of each tile
        foreach (GameObject tile in painting)
        {
            if (tile.GetComponent<SpriteRenderer>()!=null)
            {
                canvas.Add(tile.GetComponent<SpriteRenderer>().color);
            }
        }

        // Raise curtain
        canDraw = false;
        finishButton.GetComponent<Animator>().SetBool("isShowing", false);
        curtain.SetBool("Raise", true);

        // Export or score the drawing
        if (imageEdit)
        {
            // Export data
            imageCont.ImportImage(canvas);
        } else {
            // Compare drawings
            StartCoroutine(scoreCont.displayScore(canvas.ToArray(), imageCont.selectedImage, playerTiles.tiles, imageCont.imageTiles.tiles));
        }
    }

    // Coroutines
    IEnumerator curtainDrop(float wait)
    {
        canvas.Clear();
        canDraw = false;
        finishButton.GetComponent<Animator>().SetBool("isShowing", false);
        curtain.SetBool("Raise", true);
        yield return new WaitForSeconds(wait);
        curtain.SetBool("Raise", false);
        finishButton.GetComponent<Animator>().SetBool("isShowing", true);
        canDraw = true;
        yield return null;
    }

    public IEnumerator reset()
    {
        // Drop curtain
        curtain.SetBool("Raise", false);
        finishButton.GetComponent<Animator>().SetBool("isShowing", false);
        canDraw = false;

        // Clear player board
        foreach (GameObject tile in playerTiles.tiles)
        {
            yield return new WaitForSeconds(0.025f);
            tile.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        // Generate new image
        if (!imageCont.generateImage()) // All images have been shown, end game
        {
            // Configure timer
            timerCont.timerActive = false;
            timerCont.showTimer = true;
            timerCont.timerText.text = "<color=white>" + timerCont.timerText.text;
            if (PlayerPrefs.GetFloat("bestTime") > timerCont.passedTime || PlayerPrefs.GetFloat("bestTime")==0)
            {
                int bestSeconds = Mathf.FloorToInt(timerCont.passedTime%60);
                int bestMinutes = Mathf.FloorToInt(timerCont.passedTime/60);
                timerCont.best = string.Format("Best: {0:00}:{1:00}", bestMinutes, bestSeconds);
                timerCont.updateTimer();
                PlayerPrefs.SetFloat("bestTime", timerCont.passedTime);
            }

            // Show endScreen and final scores
            scoreCont.setHighscore();
            endScreen.SetActive(true);
            score.text = $"Score: {scoreCont.score}";
            highScore.text = $"Highscore: {scoreCont.highScore}";

            // Stop the game
            canDraw = false;
            selectedColor = Color.white;
            yield break;
        }

        // Reset
        StartCoroutine(curtainDrop(curtainWaitTime));
        yield return null;
    }
}