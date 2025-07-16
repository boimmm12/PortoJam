using UnityEngine;

public class Player : MonoBehaviour
{
    public float laneSwitchSpeed = 10f;

    private Vector2[] lanePositions = new Vector2[3];
    private int currentLane = 1;

    void Start()
    {
        Vector2 startPos = transform.position;

        lanePositions[0] = startPos + new Vector2(1.25f, 2f);   // Atas
        lanePositions[1] = startPos + new Vector2(0f, 0f);   // Tengah
        lanePositions[2] = startPos + new Vector2(-1.25f, -2f); // Bawah
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLane > 0)
            currentLane--;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLane < 2)
            currentLane++;

        Vector2 targetPos = lanePositions[currentLane];
        transform.position = Vector2.MoveTowards(transform.position, targetPos, laneSwitchSpeed * Time.deltaTime);
    }
}
