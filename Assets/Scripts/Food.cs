using UnityEngine;

public class Foods : MonoBehaviour
{
    // food, spikes och portals skulle defentivt bli en enkel collictible class men har inte haft tid
    [SerializeField] private Collider2D gridArea;
    
    private void Start()
    {
        RandomizePosition();
    }
    public void RandomizePosition()
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
        RandomizePosition();
    }

}
