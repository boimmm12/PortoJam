using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QTESystem : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;
    public GameObject finalSuccessImage;
    public PuzzleUI puzzleUI;

    private List<char> targetKeys = new List<char>();
    private int currentKeyIndex = 0;
    public QTEItemUI[] qteItems;

    private bool waitingForInput = false;
    public bool isPassed = false;

    void OnEnable()
    {
        isPassed = false;
        waitingForInput = false;
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
        finalSuccessImage.SetActive(false);
    }

    void Update()
    {
        if (!waitingForInput)
        {
            GenerateKeys();
        }

        if (waitingForInput && Input.anyKeyDown)
        {
            if (currentKeyIndex < targetKeys.Count)
            {
                char expectedKey = targetKeys[currentKeyIndex];

                if (Input.GetKeyDown(expectedKey.ToString().ToLower()))
                {
                    Debug.Log("Tombol benar ditekan");
                    qteItems[currentKeyIndex].MarkCorrect();
                    currentKeyIndex++;
                    UpdateDisplay();

                    if (currentKeyIndex >= targetKeys.Count)
                    {
                        waitingForInput = false;
                        StartCoroutine(ShowFinalAndClose());
                    }
                }
                else
                {
                    KeyPressing(false);
                }
            }
        }
    }

    void GenerateKeys()
    {
        targetKeys.Clear();
        currentKeyIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            char randomChar = (char)Random.Range(65, 91); // A-Z
            targetKeys.Add(randomChar);
        }

        UpdateDisplay();
        waitingForInput = true;
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < qteItems.Length; i++)
        {
            if (i < targetKeys.Count)
            {
                qteItems[i].SetLetter(targetKeys[i], i < currentKeyIndex);
            }
            else
            {
                qteItems[i].SetLetter(' ', false);
            }
        }
    }

    void KeyPressing(bool correct)
    {
        waitingForInput = false;

        if (correct)
        {
            PassBox.GetComponent<Text>().text = "Pass!";
            isPassed = true;
            finalSuccessImage.SetActive(true);
        }
        else
        {
            PassBox.GetComponent<Text>().text = "Fail!";
        }
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
        finalSuccessImage.SetActive(true);
    }
    IEnumerator ShowFinalAndClose()
    {
        PassBox.GetComponent<Text>().text = "Pass!";
        isPassed = true;
        finalSuccessImage.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        puzzleUI.ClosePuzzle();
    }
}
