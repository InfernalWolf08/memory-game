using UnityEngine;
using UnityEngine.UI;

public class PlayerDraw : MonoBehaviour
{
    [Header("Color Data")]
    public Color32 selectedColor = Color.white;

    // Custom functions
    public void changeColor(GameObject button)
    {
        selectedColor = button.GetComponent<Image>().color;
    }
}