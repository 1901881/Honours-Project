using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColour : MonoBehaviour
{
    //Changing Player Color
    public Material materialRend;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        //materialRend = GetComponent<SpriteRenderer>().material;
        originalColor = materialRend.color;
       // materialRend.SetVector("_EmissionColor", originalColor * 10);
    }

    public void ChangeMaterialColourFunc(bool isChangeColour)
    {
        if(isChangeColour)
        {
            //materialRend.color = Color.white;
            materialRend.SetColor("_Color", Color.white);
        }
        else
        {
            //materialRend.color = originalColor;
            materialRend.SetColor("_Color", originalColor);
        }
    }
}
