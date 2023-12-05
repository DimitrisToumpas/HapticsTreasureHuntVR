using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabFromUI : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
 

    private void Start()
    {
        // Add XRGrabInteractable component to the GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
            return;
        }
        
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Skip the grabbing process if it's not in a slot or doesn't have an Item component
        if (gameObject.GetComponent<Item>()==null) return;

        if (gameObject.GetComponent<Item>().inSlot)
        {
            gameObject.GetComponentInParent<Slot>().ItemInSlot =null;
            gameObject.transform.parent = null;

            Item itemComponent = GetComponent<Item>();
            gameObject.GetComponent<Item>().inSlot=false;
            gameObject.GetComponent<Item>().currentSlot.ResetColor();
            gameObject.GetComponent<Item>().currentSlot=null;
        }
    }
  
}
