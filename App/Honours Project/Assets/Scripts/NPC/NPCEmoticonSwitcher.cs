using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEmoticonSwitcher : MonoBehaviour
{
    public Sprite fightSprite;
    public Color fightColour;
    public Sprite flightSprite;
    public Color flightColour;
    public Sprite freezeSprite;
    public Color freezeColour;
    public Sprite flopSprite;
    public Color flopColour;
    public Sprite fawnSprite;
    public Color fawnColour;
    public Sprite neutralSprite;
    public Color neutralColour;
    public Sprite suprisedSprite;
    public Color suprisedColour;

    public GameObject NPC;
    public GameObject faceVisor;

    public void Start()
    {
        NPC.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
        faceVisor.gameObject.GetComponent<SpriteRenderer>().color = neutralColour;
    }

    public void SwitchEmote(int faceNum)
    {
        switch (faceNum)
        {
            case 0:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = fightSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = fightColour;
                break;
            case 1:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = flightSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = flightColour;
                break;
            case 2:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = freezeSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = freezeColour;
                break;
            case 3:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = flopSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = flopColour;
                break;
            case 4:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = fawnSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = fawnColour;
                break;
            case 5:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = neutralColour;
                break;
            case 6:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = suprisedSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = suprisedColour;
                break;
            default:
                NPC.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = neutralColour;
                break;
        }
    }
}
