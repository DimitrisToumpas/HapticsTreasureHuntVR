using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.Collections;

public class gemFinder : MonoBehaviour
{
    public Text pointsText; // Reference to the UI Text component
    private string gemTag = "Gem"; // Set the tag for your gem objects in the Unity Editor
    public float maxDistance; // Define the maximum distance for haptic feedback
    public XRDirectInteractor controller;
    private bool isFound;
    private float WaitTime;
    public float minWaitTime = 2f;
    public float maxWaitTime = 6f;
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
                //Debug.Log("Closest Gem: " + closestGem.name);

                //get Vibration Intensity and how long the gap is
                float vibrationIntensity = GetVibrationIntensity(distance);
                //float vibrationDuration = GetVibrationDuration(distance);
                float vibrationPatternGap = GetVibrationPatternWaitTime(distance);

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
                            // Start the coroutine to handle the vibration pattern with a gap
                            StartCoroutine(VibrationPattern(vibrationIntensity, vibrationPatternGap));
                            
                            // Debug.Log("Distance= "+distance);
                            //Debug.Log("Intensity= "+vibrationIntensity);
                            //controller.SendHapticImpulse(vibrationIntensity, 0.5f);
                        }
                    }
                }
            }
        }
    }
    //vibration!
    private float GetVibrationIntensity(float distance)
    {
        float hapticStrength = 1f - Mathf.Clamp01(distance / 10f); // Assuming 20 units as a reference distance


        return hapticStrength;
    }


    private float GetVibrationPatternWaitTime(float distance)
    {       
        
        

        if (distance<=50f)
        {
            WaitTime=5f;
        } else if(distance<=30f)
        {
            WaitTime=3f;
        }
        else if (distance<=20f)
        {
            WaitTime=minWaitTime;
        }
        else
            WaitTime=maxWaitTime;

        return WaitTime;
    }

    private float GetVibrationDuration(float distance)
    {
        float duration;
        float minDuration = 0.5f;
        float maxDuration = 2f;
        if (distance<5) {

            duration=minDuration;
        } else if (distance<50) 
        {
            duration=1f;
        }

            duration=maxDuration;

        return duration;
    }


    private IEnumerator VibrationPattern(float intensity, float vibrationPatternGap)
    {
        // Trigger the first vibration
        controller.SendHapticImpulse(intensity, 0.5f);

        // Wait for a short duration
        yield return new WaitForSeconds(0.5f);

        // Pause before the next vibration
        yield return new WaitForSeconds(vibrationPatternGap);

        // Trigger the second vibration
        controller.SendHapticImpulse(intensity, 0.5f);
    }

    //gem discovery!
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
    