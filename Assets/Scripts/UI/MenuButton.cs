using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MenuButton : MenuUI
{
    private Image image;
    private Color initColor;
    private void Awake()
    {
        image = GetComponent<Image>();
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
    public void OnClick() 
    {

    }
}