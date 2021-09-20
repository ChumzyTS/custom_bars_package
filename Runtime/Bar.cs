using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    // Set Values in Editor
    [HideInInspector]
    public bool manualValues;
    [HideInInspector]
    public float max;
    [HideInInspector]
    public float current;

    // Default Required Bar Stuff
    public GameObject background;
    public GameObject bar;

    private RectTransform backgroundRect;
    private RectTransform barRect;

    // Anchors decide what side the bar should snap to
    [HideInInspector]
    public int anchorIdx;
    [HideInInspector]
    public string[] anchorArray = { "Left", "Middle", "Right" };
    [HideInInspector]
    public string anchor;

    // Has Text Display
    [HideInInspector]
    public bool textDisplay;

    [HideInInspector]
    public Text textObject;

    [HideInInspector]
    public int textStyleIdx;
    [HideInInspector]
    public string[] textStyleArray = { "[Cur]", "[Cur][Max]", "[Perc]%", "Custom"};
    [HideInInspector]
    public string textStyle;
    [HideInInspector]
    public string actualStyle;
    [HideInInspector]
    public int decimalCount;


    private bool validBar = false;

    public void Awake()
    {

        if (background != null && bar != null)
        {
            GetRect();
        }
        else
        {
            Debug.LogWarning("Bar \"" + gameObject.name + "\" is missing a required bar object!");
        }
    }

    private void GetRect()
    {
        backgroundRect = background.GetComponent<RectTransform>();
        barRect = bar.GetComponent<RectTransform>();

        if (backgroundRect != null && barRect != null)
        {
            validBar = true;
        }
        else
        {
            Debug.LogWarning("Bar \"" + gameObject.name + "\" is missing a bar RectTransform!");
        }
    }

    public void UpdateBar(float newValue)
    {
        if (validBar)
        {
            // Set Current Value
            current = Mathf.Clamp(newValue, 0, max);

            // Calculate Size
            float barPercent = current / max;
            float barWidth = backgroundRect.sizeDelta.x * barPercent;

            // Set Bar Size
            barRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barWidth);

            // Align Bar Based Off Anchor
            if (anchor == "Left")
            {
                barRect.SetPositionAndRotation(new Vector3(backgroundRect.position.x - ((backgroundRect.sizeDelta.x - barWidth) / 2), backgroundRect.position.y, backgroundRect.position.z), backgroundRect.rotation);
            }
            else if (anchor == "Right")
            {
                barRect.SetPositionAndRotation(new Vector3(backgroundRect.position.x + ((backgroundRect.sizeDelta.x - barWidth) / 2), backgroundRect.position.y, backgroundRect.position.z), backgroundRect.rotation);
            }
            else
            {
                barRect.SetPositionAndRotation(backgroundRect.position, backgroundRect.rotation);

            }

            // Set Text Display
            if (textDisplay && textObject != null)
            {
                string displayedText = actualStyle;

                displayedText = displayedText.Replace("[Cur]", current.ToString());
                displayedText = displayedText.Replace("[Max]", max.ToString());

                if (actualStyle.Contains("[Perc]"))
                {
                    if (decimalCount > 0)
                    {
                        displayedText = displayedText.Replace("[Perc]", (Mathf.Floor(barPercent * Mathf.Pow(10, decimalCount) * 100) / Mathf.Pow(10, decimalCount)).ToString());
                    }
                    else
                    {
                        displayedText = displayedText.Replace("[Perc]", Mathf.Floor(barPercent * 100).ToString());
                    }
                }

                textObject.text = displayedText;
            }
        }
    }


    public void SetMax(float newMax, bool setCurrent = false)
    {
        max = newMax;

        if (setCurrent)
        {
            current = newMax;
        }

        UpdateBar(current);
    }
}
