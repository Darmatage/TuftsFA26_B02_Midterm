using UnityEngine;
using UnityEngine.UI;

public class SliderControllerLevel3 : MonoBehaviour
{
    public Slider colorSlider;
    public float speed = 0.5f;
    public Image fillImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colorSlider.value = Mathf.PingPong(Time.time * speed, 1f); //moves the value back and forth

        float value = colorSlider.normalizedValue;
        fillImage.color = Color.Lerp(Color.green, Color.red, value);

    }
}
