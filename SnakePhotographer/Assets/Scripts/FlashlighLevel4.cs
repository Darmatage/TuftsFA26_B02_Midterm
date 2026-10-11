using UnityEngine;

public class FlashlighLevel4 : MonoBehaviour
{
    public Light flashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        
    }
}
