using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Collider2D gridArea;

    private bool hasSpawned = false;
    public void Spawn()
    {
        if (hasSpawned) return;
        gameObject.SetActive(true);
        RandomizePosition();
        hasSpawned = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        hasSpawned = false;
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hide();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomizePosition();
    }
}
