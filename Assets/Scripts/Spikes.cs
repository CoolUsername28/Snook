using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] private Collider2D gridArea;
    private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        RandomizePosition();
    }
    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TriggerEnter");
        if (collision.gameObject.CompareTag("Food")) RandomizePosition();
        else if (collision.gameObject.CompareTag("Portal")) RandomizePosition();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("TriggerStay");
        if (collision.gameObject.CompareTag("Food")) RandomizePosition();
        else if (collision.gameObject.CompareTag("Portal")) RandomizePosition();
    }
}
