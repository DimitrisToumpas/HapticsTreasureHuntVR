using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class gemFinder : MonoBehaviour
{
    public Text pointsText; // Reference to the UI Text component
    private string gemTag = "Gem"; // Set the tag for your gem objects in the Unity Editor
    public float minDuration = 0.1f;
    public float maxDuration = 0.5f;
    public float maxDistance = 2f; // Define the maximum distance for haptic feedback
    public XRDirectInteractor controller;
    private bool isFound;
    private int points = 0;

    // Dictionary to track discovered gems
    private Dictionary<GameObject, bool> discoveredGems = new Dictionary<GameObject, bool>();



    private void Update()
    {
        // Find all game objects with the specified tag
        GameObject[] gems = GameObject.FindGameObjectsWithTag(gemTag);

        // Find the closest gem
        GameObject closestGem = GetClosestGem(gems);

        GrabAndPoints(closestGem);

        if (closestGem != null && isFound==false)
        {
            // Calculate the distance between the closest gem and the hand controller
            float distance = Vector3.Distance(transform.position, closestGem.transform.position);

            if (distance <= maxDistance)
            {
                // Log the name of the closest gem to the console
                Debug.Log("Closest Gem: " + closestGem.name);

                // Adjust vibration based on distance
                float vibrationIntensity = Mathf.Lerp(0f, 1f, 1f - Mathf.Clamp01(distance / maxDistance));
                float vibrationDuration = Mathf.Lerp(minDuration, maxDuration, Mathf.Clamp01(distance / maxDistance));

                if (closestGem != null)
                {
                    // Check if the closest gem object has a renderer
                    Renderer gemRenderer = closestGem.GetComponent<Renderer>();

                    if (gemRenderer != null && !DiscoveredGem(closestGem))
                    {
                        // Check the material name of the gem for transparency
                        Material gemMaterial = gemRenderer.material;

                        if (IsMaterialTransparent(gemMaterial))
                        {
                            // Trigger vibration on the hand controller
                            controller.SendHapticImpulse(vibrationIntensity, vibrationDuration);
                        }
                    }
                }
            }
        }
    }

    private GameObject GetClosestGem(GameObject[] gems)
    {
        GameObject closestGem = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject gem in gems)
        {
            float distance = Vector3.Distance(transform.position, gem.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGem = gem;
            }
        }

    

        return closestGem;
    }

    private void GrabAndPoints(GameObject closestGem) 
    {
        if (closestGem != null)
        {
            XRBaseInteractable interactable = closestGem.GetComponent<XRBaseInteractable>();

            if (interactable != null && interactable.isSelected)
            {
                // Check if the gem is named "ruby"
                if (closestGem.name.ToLower().StartsWith("ruby") && !DiscoveredGem(closestGem))
                {
                    // Award 10 points
                    points += 10;
                    UpdatePointsText();

                    // Mark the gem as discovered
                    discoveredGems[closestGem] = true;

                }
            }
        }
    }
    void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }
    bool IsMaterialTransparent(Material material)
    {
        // Check if the material name contains "transparent"
        return material != null && material.name.ToLower().Contains("transparent");
    }

    bool DiscoveredGem(GameObject gem)
    {
        // Check if the gem is in the dictionary and marked as discovered
        return discoveredGems.ContainsKey(gem) && discoveredGems[gem];
    }

}
    