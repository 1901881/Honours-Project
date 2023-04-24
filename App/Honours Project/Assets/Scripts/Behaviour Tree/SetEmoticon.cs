using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("0 - Fight, 1 - Flight, 2 - Freeze, 3 - Flop, 4 - Fawn, 5 - Neutral")]

//Sets the NPCs emoticon
public class SetEmoticon : Action
{
    public int intValue;

    public override TaskStatus OnUpdate()
    {
        gameObject.GetComponent<NPCEmoticonSwitcher>().SwitchEmote(intValue);
        return TaskStatus.Success;
    }
}
