using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BotEmoticonSwitcher : Action
{
    public Sprite fightSprite;
    public Sprite flightSprite;
    public Sprite freezeSprite;
    public Sprite neutralSprite;

    public GameObject bot;
    public GameObject faceVisor;

    public int faceNum;

    // Start is called before the first frame update
  /*  void Start()
    {
        bot.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
        faceVisor.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }*/

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        //get stress index
        //switch
        switch (faceNum)
        {
            case 0:
                bot.gameObject.GetComponent<SpriteRenderer>().sprite = fightSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                bot.gameObject.GetComponent<SpriteRenderer>().sprite = flightSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 2:
                bot.gameObject.GetComponent<SpriteRenderer>().sprite = freezeSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                bot.gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
                faceVisor.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
        return TaskStatus.Success;

/*        Color tmp = faceVisor.gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        faceVisor.gameObject.GetComponent<SpriteRenderer>().color = tmp;*/

    }
}
