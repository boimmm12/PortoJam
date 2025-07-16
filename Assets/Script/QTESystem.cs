using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QTESystem : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;

    private char targetKey;
    private bool waitingForKey = false;
    private bool isCountingDown = false;
    public StaminaSystem staminaSystem;
    public float staminaPenalty = 30f;
    public bool isPassed = false;
    public PuzzleUI puzzleUI;


    void OnEnable()
    {
        isPassed = false;
        waitingForKey = false;
        isCountingDown = false;
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
    }

    void Update()
    {
        if (!waitingForKey)
        {
            GenerateRandomKey();
        }

        if (waitingForKey && Input.anyKeyDown)
        {
            // Bandingkan dengan huruf target
            if (Input.GetKeyDown(targetKey.ToString().ToLower()))
            {
                StartCoroutine(KeyPressing(true));
            }
            else
            {
                StartCoroutine(KeyPressing(false));
            }
        }
    }

    void GenerateRandomKey()
    {
        targetKey = (char)Random.Range(65, 91); // ASCII 'A'(65) to 'Z'(90)
        DisplayBox.GetComponent<Text>().text = "[" + targetKey + "]";
        waitingForKey = true;
        isCountingDown = true;
        StartCoroutine(CountDown());
    }

    IEnumerator KeyPressing(bool correct)
    {
        waitingForKey = false;
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
    }


    IEnumerator CountDown()
    {
        float waitTime = puzzleUI != null ? puzzleUI.duration : 3f;
        yield return new WaitForSeconds(waitTime);

        if (isCountingDown)
        {
            isCountingDown = false;
            waitingForKey = false;
            PassBox.GetComponent<Text>().text = "Timeout!";
            Debug.LogWarning("Timeout");

            if (staminaSystem != null)
                staminaSystem.ReduceStamina(staminaPenalty);

            if (puzzleUI != null)
                puzzleUI.ClosePuzzle();
        }
    }


}
