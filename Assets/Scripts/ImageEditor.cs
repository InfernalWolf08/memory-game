using System.Collections.Generic;
using UnityEngine;

public class ImageEditor : MonoBehaviour
{
    public ImageController imageCont;
    public int index;
    public Color32 oldColor;
    public Color32 newColor;

    void Start()
    {
        for (int i=0; i<imageCont.images[index].imageColors.Length; i++)
        {
            Color32 color = imageCont.images[index].imageColors[i];
            if (color.Equals(oldColor))
            {
                imageCont.images[index].imageColors[i] = newColor;
            }
        }
    }
}