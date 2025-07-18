using System;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    [Header("Image Data")]
    public List<ImageArray> images = new List<ImageArray>();

    // Custom functions
    public void ImportImage(List<Color32> colors)
    {
        ImageArray image = new ImageArray();
        image.imageColors = colors.ToArray();
        images.Add(image);
    }
}

[System.Serializable]
public class ImageArray
{
    public Color32[] imageColors;
}