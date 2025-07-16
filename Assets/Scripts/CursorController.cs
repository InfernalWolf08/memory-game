using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [Header("Cursor")]
    public Texture2D cursorTex;
    public Texture2D defaultCursorTex;

    [Header("Image")]
    public Image cursor;

    /*void Start()
    {
        // Initialize
        cursorTex.SetPixels32(defaultCursorTex.GetPixels32());
        cursorTex.Apply();
        Cursor.SetCursor(null, new Vector2(cursorTex.width/2, cursorTex.height/2), CursorMode.Auto);
    }*/

    void Update()
    {
        // Make a UI image the cusor
        cursor.transform.position = Input.mousePosition;
    }

    // Actual unity cursor
    public void setCursorColor(Color32 mainColor)
    {
        // Initialize
        print("called");
        Color32[] colors = defaultCursorTex.GetPixels32();

        // Set the colors to the selected color
        for (int i=0; i<colors.Length; i++)
        {
            if (colors[i].a!=0 && (colors[i].r!=32 && colors[i].g!=32 && colors[i].b!=32))
            {
                colors[i] = mainColor;
            }
        }

        // Apply color
        cursorTex.SetPixels32(colors);
        cursorTex.Apply();

        // Apply cursor
        // Cursor.SetCursor(cursorTex, new Vector2(cursorTex.width/2, cursorTex.height/2), CursorMode.Auto);
    }
}