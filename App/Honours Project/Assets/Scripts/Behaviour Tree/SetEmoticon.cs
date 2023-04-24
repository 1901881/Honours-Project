using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("0 - Default, 1 - Fight, 2 - Flight, 3 - Freeze")]
public class SetEmoticon : Action
{
    /*[System.NonSerializedAttribute()]
    [Tooltip("The bool value to set")]*/

    public int intValue;

    //public NPCEmoticonSwitcher npcEmoticonSwitcherScript;
    public override TaskStatus OnUpdate()
    {
        gameObject.GetComponent<NPCEmoticonSwitcher>().SwitchEmote(intValue);
        //npcEmoticonSwitcherScript.SwitchEmote(intValue);
        return TaskStatus.Success;
    }
}
