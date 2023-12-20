using UnityEngine;
using UnityEngine.InputSystem;

public class LightController : MonoBehaviour
{
    public InputActionProperty LightSwitchButton;
    bool isLightOn = true;
    private Light spotLight;
    // Start is called before the first frame update
    void Start()
    {
        spotLight = gameObject.GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (LightSwitchButton.action.WasPressedThisFrame())
        {
            ToggleLight();
        }
    }
    void ToggleLight()
    {
        isLightOn = !isLightOn;
        spotLight.enabled = isLightOn;
    }

}
