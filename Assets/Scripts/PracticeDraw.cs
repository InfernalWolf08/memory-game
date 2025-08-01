using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PracticeDraw : MonoBehaviour
{
    [Header("Drawing")]
    private Camera cam;
    private CursorController cursor;
    public Color32 selectedColor = Color.white;
    public bool canDraw;
    public List<Color32> canvas = new List<Color32>();

    [Header("Displays")]
    public TileGenerator canvasTiles;
    public ImageController imageTiles;

    [Header("UI")]
    public GameObject curtain;

    void Start()
    {
        // Initialize
        cam = GetComponent<Camera>();
        cursor = GetComponent<CursorController>();
        Cursor.visible = false;
        canDraw = true;
    }

    void Update()
    {
        // Change color
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
    }

    // Custom functions
    public void changeColor(GameObject button)
    {
        selectedColor = button.GetComponent<Image>().color;
    }

    public void toggleCover()
    {
        curtain.SetActive(!curtain.activeSelf);
        canDraw = !curtain.activeSelf;
    }

    public void toggleReturn(GameObject returnScreen)
    {
        returnScreen.SetActive(!returnScreen.activeSelf);
    }

    public void checkDrawing()
    {
        if (canDraw)
        {
            // Initialize
            canDraw = false;

            // Get the player's drawing
            canvas.Clear();
            canvas = getDrawing();

            // Parse the players drawing
            StartCoroutine(score());
        }
    }

    List<Color32> getDrawing()
    {
        List<Color32> colors = new List<Color32>();

        foreach (GameObject tile in canvasTiles.tiles)
        {
            colors.Add(tile.GetComponent<SpriteRenderer>().color);
        }

        return colors;
    }

    // Coroutines
    IEnumerator score()
    {
        // Initialize
        Color32[] image = imageTiles.selectedImage;
        int score = 0;
        
        for (int i=0; i<canvas.Count; i++)
        {
            // Compare colors
            Color32 originalCanvasColor = canvas[i];
            Color32 originalImageColor = image[i];
            bool correct = originalCanvasColor.Equals(originalImageColor);

            if (correct)
            {
                // Score
                score += 1;

                // Animation
                canvasTiles.tiles[i].GetComponent<SpriteRenderer>().color = Color.black;
                imageTiles.imageTiles.tiles[i].GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(0.05f);
                canvasTiles.tiles[i].GetComponent<SpriteRenderer>().color = originalCanvasColor;
                imageTiles.imageTiles.tiles[i].GetComponent<SpriteRenderer>().color = originalImageColor;
            } else {
                // Animation
                canvasTiles.tiles[i].GetComponent<SpriteRenderer>().color = originalImageColor;
                yield return new WaitForSeconds(0.25f);
            }
        }

        // Finish
        canDraw = true;
        yield return null;
    }
}