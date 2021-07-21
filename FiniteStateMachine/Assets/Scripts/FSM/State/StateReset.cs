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
        Debug.Log("休息开始了time:" + Time.realtimeSinceStartup + "   energy:" + _player._energy);
    }

    public override void OnExecute()
    {
        if (!_player.MoveTo(StateEnum.RESET))
        {
            return;
        }
        // 饥饿感增量
        _player.SenseHunger(0.6f);
        // 要写作业的强迫性增量
        _player.NeedHomeWork(1.5f);
        // 想打篮球的渴望度增量
        _player.WantBasketball(1);
        // 体力增量
        _player.Energy(3);
    }

    public override void OnExit()
    {
        Debug.Log("休息结束了美美的睡了一觉time:" + Time.realtimeSinceStartup);
    }
}
