using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTex;

    public void setCursorColor(Color32 mainColor)
    {
        // Initialize
        Color32[] colors = new Color32[1024];

        cursorTex.SetPixels32(colors);
    }
}