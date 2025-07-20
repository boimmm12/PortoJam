using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float laneSwitchSpeed;
    public float normalspeed = 10f;
    public float depletedspeed = 2f;
    private LaneObject laneObject;

    private Vector2[] lanePositions = new Vector2[3];
    private int currentLane = 1;

    void Start()
    {
        Vector2 startPos = transform.position;
        laneSwitchSpeed = normalspeed;

        lanePositions[0] = startPos + new Vector2(1.5f, 1.3f);   // Atas
        lanePositions[1] = startPos + new Vector2(0f, 0f);   // Tengah
        lanePositions[2] = startPos + new Vector2(-0.9f, -1.1f); // Bawah

        laneObject = GetComponent<LaneObject>();
        if (laneObject != null)
            laneObject.SetLane(currentLane);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLane > 0)
            currentLane--;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLane < 2)
            currentLane++;

        Vector2 targetPos = lanePositions[currentLane];
        transform.position = Vector2.MoveTowards(transform.position, targetPos, laneSwitchSpeed * Time.deltaTime);

        if (laneObject != null)
            laneObject.SetLane(currentLane);
    }
    public void OnStaminaDepleted()
    {
        laneSwitchSpeed = depletedspeed;
    }
}
