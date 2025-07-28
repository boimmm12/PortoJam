using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QTEItemUI : MonoBehaviour
{
    [Header("Object References")]
    public Image iconImage;
    public Image letterImage;

    [Header("Icon Buah")]
    public Sprite iconDefault;
    public Sprite iconCorrect;

    [Header("Sprites per huruf")]
    public Sprite defaultSprite_b;
    public Sprite defaultSprite_d;
    public Sprite defaultSprite_h;
    public Sprite defaultSprite_k;
    public Sprite defaultSprite_m;
    public Sprite defaultSprite_y;

    private Dictionary<char, Sprite> defaultSprites;
    private char currentChar;

    private bool initialized = false;

    public void InitSprites()
    {
        if (initialized) return;

        defaultSprites = new Dictionary<char, Sprite>() {
        {'b', defaultSprite_b}, {'d', defaultSprite_d}, {'h', defaultSprite_h},
        {'k', defaultSprite_k}, {'m', defaultSprite_m}, {'y', defaultSprite_y}
    };

        initialized = true;

        if (iconImage != null && iconDefault != null)
            iconImage.sprite = iconDefault;
    }

    public void SetLetter(char c)
    {
        InitSprites();

        currentChar = char.ToLower(c);

        if (letterImage != null && defaultSprites.ContainsKey(currentChar))
        {
            letterImage.overrideSprite = defaultSprites[currentChar];
        }
    }

    public void MarkCorrect()
    {
        InitSprites();

        if (iconImage != null && iconCorrect != null)
        {
            iconImage.overrideSprite = iconCorrect; 
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void Clear()
    {
        letterImage.sprite = null;
        iconImage.sprite = iconDefault;
    }
}
