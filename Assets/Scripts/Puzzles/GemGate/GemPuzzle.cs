using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GemPuzzle : MonoBehaviour
{
    public static bool wallGateOpen = false;
    public static bool RubyInPlace = false;
    public static bool Diamondinplace = false;
    public static bool SapphireInPlace = false;



    private void Update()
    {
        CheckPedestalRuby();
        CheckPedestalDiamond();
        CheckPedestalSapphire();
        WallsOpen();
    }

    void CheckPedestalRuby()
    {
        // Get the GemHole2 object
        Transform pedestalRuby = transform.Find("pedestalRuby");

        if (pedestalRuby != null)
        {
            XRSocketInteractor socket;
            // Check if GemHole2 has an XRSocketInteractor component
            socket = pedestalRuby.GetComponent<XRSocketInteractor>();

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
                            RubyInPlace=true;
                            // Do something when the green_key interactable is attached to GemHole2
                            Debug.Log("PedestalRuby has XRGrabInteractable with tag green_key attached.");
                        }
                    }
                }
            }
        }
    }

    void CheckPedestalDiamond()
    {
        // Get the GemHole2 object
        Transform pedestalDiamond = transform.Find("pedestalDiamond");

        if (pedestalDiamond != null)
        {
            XRSocketInteractor socket;
            // Check if GemHole2 has an XRSocketInteractor component
            socket = pedestalDiamond.GetComponent<XRSocketInteractor>();

            if (socket != null)
            {
                // Check if the attached interactor has XRGrabInteractable component
                IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
                if (obj != null)
                {

                    if (obj is MonoBehaviour monoBehaviour)
                    {
                        GameObject gameObj = monoBehaviour.gameObject;

                        if (gameObj.tag == "ice_key")
                        {
                            Diamondinplace = true;
                            // Do something when the green_key interactable is attached to GemHole2
                            Debug.Log("PedestalDiamond has XRGrabInteractable with tag ice_key attached.");
                        }
                    }
                }
            }
        }
    }
    void CheckPedestalSapphire()
    {
        // Get the GemHole2 object
        Transform pedestalSapphire = transform.Find("pedestalSapphire");

        if (pedestalSapphire != null)
        {
            XRSocketInteractor socket;
            // Check if GemHole2 has an XRSocketInteractor component
            socket = pedestalSapphire.GetComponent<XRSocketInteractor>();

            if (socket != null)
            {
                // Check if the attached interactor has XRGrabInteractable component
                IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
                if (obj != null)
                {

                    if (obj is MonoBehaviour monoBehaviour)
                    {
                        GameObject gameObj = monoBehaviour.gameObject;

                        if (gameObj.tag == "blue_key")
                        {
                            SapphireInPlace = true;
                            // Do something when the green_key interactable is attached to GemHole2
                            Debug.Log("PedestalSapphire has XRGrabInteractable with tag blue_key attached.");
                        }
                    }
                }
            }
        }
    }

    void WallsOpen()
    {
        if(RubyInPlace == true && Diamondinplace == true && SapphireInPlace == true){
            wallGateOpen = true;
            Debug.Log("wallGateOpen is true");
        }
            
    }
}
