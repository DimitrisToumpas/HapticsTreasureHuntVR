using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    public Image SlotImage;
    Color originalColor;
    public InputActionProperty gripRelease;

    private void Start()
    {
        SlotImage = GetComponentInChildren<Image>();
        originalColor = SlotImage.color;
    }

    // Your existing OnTriggerStay method
    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;
        GameObject obj = other.gameObject;
       
        if (!IsItem(obj)) return;
        Collider slotCollider = GetComponent<Collider>();
        Collider objCollider = obj.GetComponent<Collider>();
        if (gripRelease.action.WasReleasedThisFrame()&& slotCollider.bounds.Intersects(objCollider.bounds))
        {
            InsertItem(obj);
        }
    }

  

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }

    void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        SlotImage.color = Color.gray;
    }

    public void DetachItem(GameObject obj)
    {
        Item itemComponent = obj.GetComponent<Item>();

        if (itemComponent != null && itemComponent.currentSlot == this)
        {
            // Detach the item from the slot
            obj.transform.SetParent(null);  
            obj.GetComponent<Rigidbody>().isKinematic = false;
            itemComponent.inSlot = false;
            itemComponent.currentSlot.ResetColor();
            itemComponent.currentSlot = null;
            ItemInSlot = null;
            
        }
    }


    public void ResetColor()
    {
        SlotImage.color = originalColor;
    }
}
