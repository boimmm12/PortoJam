using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameController.Instance.isPuzzle) return;

        float speed = GameController.Instance.GlobalSpeed;
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit puzzle!");
            GameController.Instance.isPuzzle = true;
            Time.timeScale = 0f;
        }
    }
}
