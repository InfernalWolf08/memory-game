using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDraw : MonoBehaviour
{
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

    void Start()
    {
        // Initialize
        cam = GetComponent<Camera>();
        cursor = GetComponent<CursorController>();
        Cursor.visible = false;
        StartCoroutine(curtainDrop(curtainWaitTime));
    }

    void Update()
    {
        // Hotkeys
        if (Input.GetKeyDown("1"))
        {
            selectedColor = new Color32(255, 0, 0, 255); // Red
        } else if (Input.GetKeyDown("2")) {
            selectedColor = new Color32(255, Convert.ToByte(127.5), 0, 255); // Orange
        } else if (Input.GetKeyDown("3")) {
            selectedColor = new Color32(255, 255, 0, 255); // Yellow
        } else if (Input.GetKeyDown("4")) {
            selectedColor = new Color32(0, 255, 0, 255); // Green
        } else if (Input.GetKeyDown("5")) {
            selectedColor = new Color32(0, 0, 255, 255); // Blue
        } else if (Input.GetKeyDown("6")) {
            selectedColor = new Color32(Convert.ToByte(127.5), 0, 255, 255); // Purple
        } else if (Input.GetKeyDown("7")) {
            selectedColor = new Color32(255, 0, 255, 255); // Pink
        } else if (Input.GetKeyDown("8")) {
            selectedColor = new Color32(Convert.ToByte(127.5), Convert.ToByte(63.75), 0, 255); // Brown
        } else if (Input.GetKeyDown("9")) {
            selectedColor = new Color32(0, 0, 0, 255); // Black
        } else if (Input.GetKeyDown("0")) {
            selectedColor = new Color32(255, 255, 255, 255); // White
        }

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

        // Export data
        UnityEngine.Object.FindFirstObjectByType<ImageController>().ImportImage(canvas);

        // Reset
        StartCoroutine(curtainDrop(0));

        // Compare drawings
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
}