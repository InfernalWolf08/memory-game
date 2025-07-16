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

    // Custom functions
    public void changeColor(GameObject button)
    {
        selectedColor = button.GetComponent<Image>().color;
        cursor.cursor.color = selectedColor;
        // cursor.setCursorColor(selectedColor);
    }
}