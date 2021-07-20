using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReset : StateBase
{
    public StateReset()
    {
        _state = StateEnum.RESET;
    }

    public override void OnEnter()
    {
        Debug.Log("我要开始休息了" + "   time:" + Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    {
        // 每帧体力恢复 2.5f
        _player.Energy(2.5f);
        // 每帧饥饿感增加 0.02f
        _player.SenseHunger(0.2f);
    }

    public override void OnExit()
    {
        Debug.Log("美美的睡了一觉，好精神，叠被子，收拾房间" + "   time:" + Time.realtimeSinceStartup);
    }
}
