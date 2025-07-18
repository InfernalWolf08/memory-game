using System;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    [Header("Image Data")]
    public List<ImageArray> images = new List<ImageArray>();
    public TileGenerator imageTiles;
    [HideInInspector] public Color32[] selectedImage;

    void Start()
    {
        // Initialize
        generateImage();
    }

    // Custom functions
    void generateImage()
    {
        // Randomly pick an image
        selectedImage = images[UnityEngine.Random.Range(0, images.Count)].imageColors;
        
        // Generate the image
        for (int i=0; i<imageTiles.tiles.Length; i++)
        {
            GameObject tile = imageTiles.tiles[i];

            if (tile.GetComponent<SpriteRenderer>()!=null)
            {
                tile.GetComponent<SpriteRenderer>().color = selectedImage[i];
            }
        }
    }

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