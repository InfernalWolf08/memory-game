using System;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    [Header("Image Data")]
    public List<ImageArray> images = new List<ImageArray>();

    public void ImportImage(List<Color32> colors)
    {
        
    }
}

[System.Serializable]
public class ImageArray
{
    public Color32[] imageColors;
}