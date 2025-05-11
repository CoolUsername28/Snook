using UnityEngine;
using UnityEngine.UI;

public class Segment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D boxCollider;
    

    public void GhostMode()
    {
        boxCollider.enabled = false;
        sprite.color = Color.cyan;
    }
    public void NormalMode()
    {
        boxCollider.enabled = true;
        sprite.color = Color.green;
    }
}
