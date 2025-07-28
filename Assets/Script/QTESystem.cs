using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QTESystem : MonoBehaviour
{
    public GameObject finalSuccessImage;
    public PuzzleUI puzzleUI;

    public QTEItemUI[] qteItems;

    private List<char> allowedKeys = new List<char> { 'b', 'd', 'h', 'k', 'm', 'y' };
    private List<char> targetKeys = new List<char>();
    private int currentKeyIndex = 0;

    private bool waitingForInput = false;
    public bool isPassed = false;

    void OnEnable()
    {
        isPassed = false;
        waitingForInput = false;
        currentKeyIndex = 0;
        finalSuccessImage.SetActive(false);

        GenerateKeys();
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
                KeyCode expectedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), expectedKey.ToString().ToUpper());

                if (Input.GetKeyDown(expectedKeyCode))
                {
                    qteItems[currentKeyIndex].MarkCorrect();
                    currentKeyIndex++;
                    UpdateDisplay();

                    if (currentKeyIndex >= targetKeys.Count)
                    {
                        waitingForInput = false;
                        StartCoroutine(ShowFinalAndClose(true));
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
            char randomChar = allowedKeys[Random.Range(0, allowedKeys.Count)];
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
                char letter = targetKeys[i];
                qteItems[i].InitSprites();
                qteItems[i].SetLetter(letter);
            }
            else
            {
                qteItems[i].Clear();
            }
        }
    }

    void KeyPressing(bool correct)
    {
        waitingForInput = false;

        if (correct)
        {
            isPassed = true;
            finalSuccessImage.SetActive(true);
        }
    }

    IEnumerator ShowFinalAndClose(bool isSuccess)
    {
        if (isSuccess)
        {
            isPassed = true;
            finalSuccessImage.SetActive(true);
        }
        else
        {
            isPassed = false;
            finalSuccessImage.SetActive(false);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        puzzleUI.ClosePuzzle();
    }
}
