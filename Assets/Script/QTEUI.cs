using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEItemUI : MonoBehaviour
{
    public Image iconImage;
    public Text letterText;

    public Sprite iconDefault;
    public Sprite iconCorrect;

    public void SetLetter(char c, bool alreadyCorrect)
    {
        letterText.text = c.ToString();

        if (!alreadyCorrect)
        {
            iconImage.sprite = iconDefault;
        }
    }



    public Color correctColor = Color.green;

    public void MarkCorrect()
    {
        if (iconCorrect != null && iconImage != null)
        {
            iconImage.sprite = iconCorrect;
        }
    }

}
