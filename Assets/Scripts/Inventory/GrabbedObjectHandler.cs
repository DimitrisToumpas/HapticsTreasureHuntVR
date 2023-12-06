using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbedObjectHandler : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
            return;
        }

        grabInteractable.selectExited.AddListener(OnGrabRelease);
    }

    private void OnGrabRelease(SelectExitEventArgs args)
    {
        // Check if the object has an Item component and is in a slot
        Item itemComponent = GetComponent<Item>();
        if (itemComponent != null && itemComponent.inSlot)
        {
            itemComponent.currentSlot.DetachItem(gameObject);
        }
    }
}
