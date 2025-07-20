using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QTESystem : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;

    public StaminaSystem staminaSystem;
    public float staminaPenalty = 30f;
    public PuzzleUI puzzleUI;

    private List<char> targetKeys = new List<char>();
    private int currentKeyIndex = 0;
    public QTEItemUI[] qteItems;

    private bool waitingForInput = false;
    private bool isCountingDown = false;
    public bool isPassed = false;

    void OnEnable()
    {
        isPassed = false;
        waitingForInput = false;
        isCountingDown = false;
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
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
                        StartCoroutine(KeyPressing(true));
                    }
                }
                else
                {
                    StartCoroutine(KeyPressing(false));
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
        isCountingDown = true;
        StartCoroutine(CountDown());
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < qteItems.Length; i++)
        {
            if (i < targetKeys.Count)
            {
                qteItems[i].SetLetter(targetKeys[i], i < currentKeyIndex); // tambahkan param status
            }
            else
            {
                qteItems[i].SetLetter(' ', false);
            }
        }
    }



    IEnumerator KeyPressing(bool correct)
    {
        waitingForInput = false;
        isCountingDown = false;

        if (correct)
        {
            PassBox.GetComponent<Text>().text = "Pass!";
            isPassed = true;
        }
        else
        {
            PassBox.GetComponent<Text>().text = "Fail!";
            if (staminaSystem != null)
                staminaSystem.ReduceStamina(staminaPenalty);
        }

        yield return new WaitForSeconds(1.5f);
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";

        if (puzzleUI != null)
            puzzleUI.ClosePuzzle();
    }

    IEnumerator CountDown()
    {
        float waitTime = puzzleUI != null ? puzzleUI.duration : 3f;
        yield return new WaitForSeconds(waitTime);

        if (isCountingDown)
        {
            isCountingDown = false;
            waitingForInput = false;
            PassBox.GetComponent<Text>().text = "Timeout!";
            Debug.LogWarning("Timeout");

            if (staminaSystem != null)
                staminaSystem.ReduceStamina(staminaPenalty);

            if (puzzleUI != null)
                puzzleUI.ClosePuzzle();
        }
    }
}
