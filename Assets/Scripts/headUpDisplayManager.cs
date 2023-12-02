using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class headUpDisplayManager : MonoBehaviour
{
    public float yPosition = 0f;
    //public float xPosition = 0f;
    public Transform head;
    public GameObject HUD;
    public float menuDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUD.transform.position = head.position + new Vector3(head.forward.x, head.forward.y+yPosition, head.forward.z).normalized* menuDistance;
        HUD.transform.rotation = Quaternion.Euler(head.rotation.eulerAngles.x, 0f, 0f);
        HUD.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        HUD.transform.forward *= -1;
    }
}
