using UnityEngine;

public class LoopingRoad : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float resetX = -20f;
    public float startX = 20f;

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= resetX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startX;
            transform.position = newPos;
        }
    }
}
