﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadTexturesOnPanel : MonoBehaviour
{
    public string[] imagesName;
    public Image[] img;
    private List<Sprite> sprites = new List<Sprite>();


    private void Awake()
    {
        gameObject.SetActive(false);
    }


    void OnEnable()
    {
        if (sprites.Capacity == 0)
        {
            for (int i = 0; i < imagesName.Length; i++)
            {
                if (!string.IsNullOrEmpty(imagesName[i]))
                {
                    string path = Path.Combine(Globals.data.videoFolder, Globals.engagementVideoFolder);
                    string filePath = Path.Combine(path, imagesName[i]);
                    sprites.Add(LoadNewSprite(filePath));
                    img[i].sprite = sprites[i];
                }
            }
        }
    }



    Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }




    Sprite ConvertTextureToSprite(Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Converts a Texture2D to a sprite, assign this texture to a new sprite and return its reference

        Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }


    Texture2D LoadTexture(string FilePath)
    {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }

        ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "L'immagine " + FilePath + " non esiste");
        return null;
        // Return null if load failed
    }
}
