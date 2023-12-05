using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;



public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    public Image SlotImage;
    Color originalColor;
    private XRBaseInteractor currentInteractor;
    private XRController currentController;


    private void Start()
    {
        SlotImage = GetComponentInChildren<Image>();
        originalColor = SlotImage.color;
    }


    private void Update()
    {
        if (ItemInSlot != null || currentController == null) return;

        if (currentController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
                    XRBaseInteractor interactor = currentController.GetComponent<XRBaseInteractor>();

            if (interactor != null)
            {
                InsertItem(interactor.gameObject);
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;
        GameObject obj = other.gameObject;
        if (!IsItem(obj)) return;

        // Get the XRGrabInteractable component from the interactable object
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        // Use XRController instead of InputDevice
        if (grabInteractable != null )
        {
            grabInteractable.onSelectEnter.AddListener(OnSelectEnter);
            grabInteractable.onSelectExit.AddListener(OnSelectExit);
        }
        else Debug.Log("Provlima");
    }

    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        XRController controller = interactor.GetComponent<XRController>();
        if (controller != null && controller.inputDevice.characteristics.HasFlag(UnityEngine.XR.InputDeviceCharacteristics.Controller))
        {
            currentController = controller;
        }
    }

    private void OnSelectExit(XRBaseInteractor interactor)
    {
        XRDirectInteractor directInteractor = currentController.GetComponent<XRDirectInteractor>();
        if (directInteractor != null && directInteractor  == currentController)
        {
            currentController = null;
        }
    }





    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }

    void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic=true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot= true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot=obj;
        SlotImage.color = Color.gray;
    }

    public void ResetColor()
    {
        SlotImage.color= originalColor;
    }
}
