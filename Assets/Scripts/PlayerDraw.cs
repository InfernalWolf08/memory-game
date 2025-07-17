using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDraw : MonoBehaviour
{
    [Header("Color Data")]
    public Color32 selectedColor = Color.white;
    public List<Color32> canvas = new List<Color32>();
    private CursorController cursor;

    void Start()
    {
        // Initialize
        cursor = GetComponent<CursorController>();
        Cursor.visible = false;
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
    }

    // Custom functions
    public void changeColor(GameObject button)
    {
        selectedColor = button.GetComponent<Image>().color;
        // cursor.setCursorColor(selectedColor);
    }
}