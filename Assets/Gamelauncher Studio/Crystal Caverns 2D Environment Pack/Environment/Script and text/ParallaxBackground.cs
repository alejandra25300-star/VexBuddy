using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;      // Reference to the player
    public float parallaxEffect;  // Speed of parallax (lower = slower)

    private float length;         // Length of the background
    private float startPos;       // Starting position of the background

    void Start()
    {
        // Calculate the starting position and length of the background
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calculate the parallax effect
        float distanceMoved = player.position.x * parallaxEffect;

        // Move the background accordingly
        float newPosition = startPos + distanceMoved;

        // Apply the new position
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);

        // Reset background position when it moves too far to the left (loop effect)
        if (newPosition > startPos + length)
        {
            startPos += length;
        }
        else if (newPosition < startPos - length)
        {
            startPos -= length;
        }
    }
}
