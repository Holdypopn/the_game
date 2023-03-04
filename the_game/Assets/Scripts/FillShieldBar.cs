using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillShieldBar : MonoBehaviour
{
    public Player player;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get rid of the beginning part of the slider (there is still a red part with 0 hp)
        if(slider.value <= slider.minValue)
            fillImage.enabled = false;

        if(slider.value > slider.minValue && !fillImage.enabled)
            fillImage.enabled = true;

        float fillValue = player.currentShield / player.maxShield;
        slider.value = fillValue;
    }
}
