using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GemPuzzle : MonoBehaviour
{
    public static bool wallGateOpen = false;



    private void Update()
    {
        CheckGemHole2();
    }

    void CheckGemHole2()
    {
        // Get the GemHole2 object
        Transform gemHole2 = transform.Find("GemHole2");

        if (gemHole2 != null)
        {
            XRSocketInteractor socket;
            // Check if GemHole2 has an XRSocketInteractor component
            socket = gemHole2.GetComponent<XRSocketInteractor>();

            if (socket != null)
            {
                // Check if the attached interactor has XRGrabInteractable component
                IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
                if (obj != null)
                {

                    if (obj is MonoBehaviour monoBehaviour)
                    {
                        GameObject gameObj = monoBehaviour.gameObject;

                        if (gameObj.tag=="green_key")
                        {
                            wallGateOpen=true;
                            // Do something when the green_key interactable is attached to GemHole2
                            Debug.Log("GemHole2 has XRGrabInteractable with tag green_key attached.");
                        }
                    }
                }
            }
        }
    }
}
