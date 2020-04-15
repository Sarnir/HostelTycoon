using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingPanel : UIPanel
{
    Hostel hostel;

    [SerializeField]
    Slider cleanlinessSlider = default;
    [SerializeField]
    Text cleanlinessValue = default;

    float updateTimer = default;

    protected override void Init()
    {
        hostel = FindObjectOfType<Hostel>();
    }

    protected override void OnOpened()
    {
        base.OnOpened();

        RefreshPanel();
    }

    private void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer > 5f)
        {
            RefreshPanel();
            updateTimer = 0f;
        }
    }

    void RefreshPanel()
    {
        float cleanliness = hostel.Qualities[HostelQuality.Cleanliness];

        cleanlinessSlider.value = cleanliness;
        cleanlinessValue.text = (int)(cleanliness * 100f) + "%";
    }
}
