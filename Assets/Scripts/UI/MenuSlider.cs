using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MenuSlider : MenuUI
{
    Slider slider;
    Image image;
    Color initColor;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        image = slider.transform.GetChild(0).GetComponent<Image>();
        initColor = image.color;
    }
    public override void OnDeselected()
    {
        image.color = initColor;
    }
    public override void OnSelected()
    {
        image.color = Color.red;
    }
    public void SlideChanged(Vector2 dir) 
    {
        slider.value += dir.y * 0.1f;
    }
}