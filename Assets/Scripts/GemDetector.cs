using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GemDetector : MonoBehaviour
{
    public float pulseDuration = 1.0f;
    public float pulseInterval = 2.0f;
    public float maxHapticIntensity = 1.0f;
    public float minHapticDistance = 0.1f;
    public XRDirectInteractor xrController;
    private bool isPulsating = false;

  
   

    private void Update()
    {
        CheckGemProximity();
    }

    private void CheckGemProximity()
    {
        // Replace 'Gem' with the tag or layer of your gems
        Collider[] gems = Physics.OverlapSphere(xrController.transform.position, 10.0f, LayerMask.GetMask("Gem"));

        if (gems.Length > 0)
        {
            float closestDistance = float.MaxValue;

            foreach (Collider gem in gems)
            {
                float distance = Vector3.Distance(xrController.transform.position, gem.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }

            if (closestDistance < minHapticDistance)
            {
                // Trigger haptic feedback based on proximity
                float normalizedIntensity = Mathf.Clamp01(1.0f - closestDistance / minHapticDistance);
                float hapticIntensity = normalizedIntensity * maxHapticIntensity;

                // Start pulsating feedback
                if (!isPulsating)
                {
                    StartCoroutine(PulsatingFeedback(hapticIntensity));
                }

            }
        }
    }

    private IEnumerator PulsatingFeedback(float intensity)
    {
        isPulsating = true;

        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < pulseDuration)
            {
                float normalizedTime = elapsedTime / pulseDuration;
                float hapticIntensity = Mathf.Lerp(0, intensity, Mathf.PingPong(normalizedTime * 2, 1));

                // Send haptic feedback to the controller
                xrController.SendHapticImpulse(hapticIntensity, 0.1f);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(pulseInterval);
        }
    }
}
