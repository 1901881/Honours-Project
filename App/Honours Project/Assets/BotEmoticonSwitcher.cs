using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEmoticonSwitcher : MonoBehaviour
{
    public Sprite neutralSprite;
    public Sprite fightSprite;
    public Sprite flightSprite;
    public Sprite freezeSprite;

    public NavAgentAI navAgentAIScript;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
    }

    // Update is called once per frame
    void Update()
    {
        //get stress index
        //switch
        switch (navAgentAIScript.stressResponseIndex)
        {
            case 0:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = fightSprite;
                break;
            case 1:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = flightSprite;
                break;
            case 2:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = freezeSprite;
                break;
            default:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
                break;
        }
    }
}
