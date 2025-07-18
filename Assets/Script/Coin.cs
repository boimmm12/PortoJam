using UnityEngine;

public class Coin : MonoBehaviour
{
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
            Debug.Log("Coin collected!");
            GameController.Instance.coinScore += 1;
            Debug.Log($"{GameController.Instance.coinScore}");
            Destroy(gameObject);
        }
    }
}
