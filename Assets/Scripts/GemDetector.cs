using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GemDetector : MonoBehaviour
{
    public float pulseDuration = 1.0f;
    public float pulseInterval = 2.0f;
    public float maxHapticIntensity = 1.0f;
    public float minHapticDistance;
    public XRDirectInteractor xrControllerLeft;
    public XRDirectInteractor xrControllerRight;
    private XRDirectInteractor xrController;
    private bool isPulsating = false;
    private Collider foundGem;  // Track the found gem

 

    private void Update()
    {
        CheckGemProximity();
    }

    private void CheckGemProximity()
    {
        // Replace 'Gem' with the tag or layer of your gems
        Collider[] gems = Physics.OverlapSphere(transform.position, 20.0f, LayerMask.GetMask("Gem"));

        Collider closestGem = GetClosestGem(gems);

        // Check if the closest gem object has a renderer
        Renderer gemRenderer = closestGem.GetComponent<Renderer>();
        // Check the material name of the gem for transparency
        Material gemMaterial = gemRenderer.material;

        // Determine the direction (left or right) based on x-coordinates
        bool isGemToRight = closestGem.transform.position.x > transform.position.x;
        bool isGemToLeft = closestGem.transform.position.x < transform.position.x;
        if (isGemToRight) { xrController=xrControllerRight; }
        else if (isGemToLeft) { xrController=xrControllerLeft; }

        if (closestGem != null && IsMaterialTransparent(gemMaterial))
        {
            

            float closestDistance = Vector3.Distance(xrController.transform.position, closestGem.transform.position);

            // Calculate the direction from the player to the gem
            Vector3 playerToGemDirection = (closestGem.transform.position - transform.position).normalized;

            // Check if the gem is in front of the player
            float dotProduct = Vector3.Dot(playerToGemDirection, transform.forward);


            if (closestDistance <= minHapticDistance && !isPulsating)
                {
                if (dotProduct> 0.5f)
                {
                    

                    StartCoroutine(PulsatingFeedback(closestDistance, xrControllerRight));
                    StartCoroutine(PulsatingFeedback(closestDistance, xrControllerLeft));
                }
                else
                    
                    StartCoroutine(PulsatingFeedback(closestDistance,xrController));
                 }
                  
        }
        else return;
    }

    private IEnumerator PulsatingFeedback(float closestDistance, XRDirectInteractor xrController)
    {
        isPulsating = true;

        float startTime = Time.time;
        float endTime = startTime + pulseDuration;

        while (Time.time < endTime)
        {
            
            float currentDistance = closestDistance;

            // Calculate the distance-dependent intensity
            float distanceDifference = minHapticDistance - currentDistance;

            // Map the distance difference to the desired intensity range (0.1, 1)
            float mappedIntensity = Mathf.Lerp(0.1f, 1.0f, Mathf.Clamp01(distanceDifference / minHapticDistance));

            //Debug.Log("Mapped Intensity: " + mappedIntensity);

            // Send haptic feedback to the controller
            xrController.SendHapticImpulse(mappedIntensity, 0.1f);

            yield return null;
        }

        // Add a delay before the coroutine can be called again
        yield return new WaitForSeconds(pulseInterval);

        // Reset isPulsating to false before starting the next pulsation cycle
        isPulsating = false;
        // Add any other cleanup or reset logic here
    }




    private Collider GetClosestGem(Collider[] gems)
    {
        Collider closestGem = null;
        float closestDistance = float.MaxValue;

        foreach (Collider gem in gems)
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


    bool IsMaterialTransparent(Material material)
    {
        // Check if the material name contains "transparent"
        return material != null && material.name.ToLower().Contains("transparent");
    }
}
