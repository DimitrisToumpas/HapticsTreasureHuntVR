using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float moveDistance = 5.0f;

    private GemPuzzle gemPuzzleScript; // Reference to the gemPuzzle script

    private Transform[] walls;

    void Start()
    {
        // Assuming the walls are direct children of this GameObject
        walls = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            walls[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        if (gemPuzzleScript.wallGateOpen) { 
        MoveWalls();
        }
    }

    void MoveWalls()
    {
        // Move each wall to the right
        foreach (Transform wall in walls)
        {
            Vector3 newPosition = wall.position + Vector3.right * moveSpeed * Time.deltaTime;
            wall.position = Vector3.MoveTowards(wall.position, newPosition, moveSpeed * Time.deltaTime);
        }

        // Check if the walls have reached the desired position
        float totalDistanceMoved = Mathf.Abs(walls[0].position.x - transform.position.x);
        if (totalDistanceMoved >= moveDistance)
        {
            // Stop moving the walls or perform additional actions
            Debug.Log("Walls moved to the desired position");
            enabled = false; // Disable this script to stop further updates
        }
    }
}
