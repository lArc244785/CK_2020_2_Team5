using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    private Slider dashSlider;
    private float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        dashSlider = GetComponent<Slider>();
    }

    public void Draw() 
    {
        OpperaterValue();
        dashSlider.value = sliderValue;
    }

    private void OpperaterValue()
    {
        PlayerControl pc = GameManger.instance.getPlayerControl();
        Vector2 dashCollTime = pc.getDashTime();
        print("CODE 234: " + dashCollTime);
        float MaxTime = dashCollTime.y;
        float currentTime =  dashCollTime.x;

        sliderValue = currentTime / MaxTime ;


    }


}
