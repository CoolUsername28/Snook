using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour, IGameStateListener
{
    [SerializeField] private BoxCollider2D collider2D;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    private float nextUpdate;
    private bool gameIsRunning = false;
    private int health = 1;
    private bool ghostTail;

    private void Awake()
    {
        UpgradeManager.upgradeAdded += upgradeAddedCallback;
    }

    private void OnDestroy()
    {

        UpgradeManager.upgradeAdded -= upgradeAddedCallback;
        
    }

    private void Start()
    {
        health = 1;
    }

    private void Update()
    {
        if (!gameIsRunning) return;
        HandleInput();
    }
    private void FixedUpdate()
    {
        if (!gameIsRunning) return;
        HandleMovement();
        HandleGhostTail();
    }
    private void HandleInput()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2.left;
            }
        }
    }

    private void HandleMovement()
    {
        // Set the new direction based on the input
        if (input != Vector2.zero)
        {
            direction = input;
        }
        //Slow down the time
        if (Time.time < nextUpdate)
        {
            return;
        }
        // Set each segment's position to be the same as the one it follows. We must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }
    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

 
    public void ResetState(int size)
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);
        // -1 since the head is already in the list
        for (int i = 0; i < (initialSize+size) - 1; i++)
        {
            Grow();
        }
         collider2D.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            GameManager.Instance.FoodCollectedCallback();
            Grow();
            if(UpgradeManager.Instance.hasUpgrade("Better Apple"))
            {
                GameManager.Instance.FoodCollectedCallback();
                Grow();
            }
        }
        else if (other.gameObject.CompareTag("Portal"))
        {
            GameManager.Instance.NextLevelCallback();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            collider2D.enabled = false;
            TakeDamage();
        }
        else if (other.gameObject.CompareTag("Tail"))
        {
            collider2D.enabled = false;
            TakeDamage();
        }
    }
    private void HandleGhostTail()
    {
        if (!ghostTail) return;
        for (int i = 0; i < segments.Count; i++)
        {
            Transform t = segments[i];

            if (i > (segments.Count / 2))
            {
                t.gameObject.GetComponent<Segment>().GhostMode();
            }
          /*  else
            {
                t.gameObject.GetComponent<Segment>().NormalMode();
            }*/
        }
    }
    private void TakeDamage()
    {
        ResetState(GameManager.Instance.score);
        health--;
        GameUI.Instance.UpdateHearts(health);
        if (health == 0)
        {
            GameManager.Instance.SetGameState(GameState.GAMEOVER);
        }

       
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        if(gameState == GameState.GAME)
        {
            ResetState(0);
            health = 1;
            speed = 5;
            if (UpgradeManager.Instance.hasUpgrade("Rocket")) speed = 7;
            if (UpgradeManager.Instance.hasUpgrade("Health Potion")) health = 2;
            GameUI.Instance.UpdateHearts(health);
            gameIsRunning = true;
        }
        else
        {
            gameIsRunning = false;
        }
    }
    private void upgradeAddedCallback(UpgradeSO sO)
    {
        if (sO.upgradeName == "Health Potion") { health += 1; GameUI.Instance.UpdateHearts(health); }
        if (sO.upgradeName == "Ghost Tail") ghostTail = true;
        

    }

}
