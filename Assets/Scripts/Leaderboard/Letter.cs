using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public TMP_Text text;
    public GameObject image;
    public char letter = 'A';
    public virtual void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.text = "" + letter;
    }
    public virtual void LetterUp()
    {
        letter = (char)(((letter - 'A' + 1) % 26) + 'A');
        text.text = "" + letter;
    }

    public virtual void LetterDown()
    {
        letter = (char)(((letter - 'A' - 1 + 26) % 26) + 'A');
        text.text = "" + letter;
    }
    public virtual void Activate() 
    {
        if(image!=null) image.SetActive(true);
    }
    public virtual void Deactivate()
    {
        if (image != null) image.SetActive(false);
    }
}
