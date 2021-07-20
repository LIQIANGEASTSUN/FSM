using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHomeWork : StateBase
{
    public StateHomeWork()
    {
        _state = StateEnum.HOMEWORK;
    }

    public override void OnEnter()
    {
        Debug.Log("开始写作业啦");
    }

    public override void OnExecute()
    { 
        // 每帧作业量不断减少 1
        _player.DoHomeWork(1);
        // 每帧需要写作业的需求降低 1
        _player.NeedHomeWork(-1);
        // 每帧体力不断减少 0.1f
        _player.Energy(0.1f);
        // 每帧饥饿感增加 0.1f
        _player.SenseHunger(0.1f);
    }

    public override void OnExit()
    {
        Debug.Log("停止写作业，作业本收起来");
    }
}
