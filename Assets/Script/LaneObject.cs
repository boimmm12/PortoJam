using UnityEngine;

public class LaneObject : MonoBehaviour
{
    public int laneIndex; // 0 = atas, 1 = tengah, 2 = bawah
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetLane(int index)
    {
        laneIndex = index;

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.sortingOrder = index switch
            {
                0 => 1,   // atas
                1 => 2,   // tengah
                2 => 3,  // bawah
                _ => 0
            };

    }
}
