using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteImporter : MonoBehaviour
{
    public Texture2D[] sprites;
    public ImageController imageCont;

    void Start()
    {
        // Iterate through the list of sprites
        foreach (Texture2D sprite in sprites)
        {
            List<Color32> convertedSprite = convertToPixels(sprite);
            imageCont.ImportImage(convertedSprite);
        }
    }

    // Convert the sprite into usable data
    List<Color32> convertToPixels(Texture2D sprite)
    {
        Color32[] colors = sprite.GetPixels32();
        
        for (int i=0; i<colors.Length; i++)
        {
            if (Convert.ToSingle(colors[i].a) < 255)
            {
                colors[i] = Color.white;
            }
        }
        
        return colors.ToList();
    }
}