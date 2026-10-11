using UnityEngine;
using UnityEngine.UI;

public class DistanceSliderController : MonoBehaviour
{
    public Transform player;
    public Transform snake;
    public Slider distanceSlider;
    public Image fillImage;

    public float minDistance = 4f;
    public float maxDistance = 6f;
    public float maxSliderDistance = 15f;

    public bool CanTakePicture { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance( //measures the distance between the player and the snake
            player.position, snake.position
        );

        //updating the slider value based on the distance
        distanceSlider.value = 1f - Mathf.Clamp01(distance / maxSliderDistance);

        CanTakePicture = distance >= minDistance && distance <= maxDistance;

        //actually change the color of the slider
        if (CanTakePicture)
        {
            fillImage.color = Color.green;
        }
        else if (distance <= maxDistance + 2f &&
                 distance >= minDistance - 1f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

}
