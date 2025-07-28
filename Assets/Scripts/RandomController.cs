using System.Collections.Generic;
using UnityEngine;

public class RandomController : MonoBehaviour
{
    public Color32[] imageColors;
    public ImageController imageCont;
    public int imageAmount;
    public int tileAmount;

    void Awake()
    {
        // Make the amount of necessary images
        for (int i=0; i<imageAmount; i++)
        {
            List<Color32> randomColors = new List<Color32>();

            // Randomize each tile
            for (int j=0; j<tileAmount; j++)
            {
                randomColors.Add(imageColors[UnityEngine.Random.Range(0, imageColors.Length)]);
            }

            imageCont.ImportImage(randomColors);
        }
    }
}