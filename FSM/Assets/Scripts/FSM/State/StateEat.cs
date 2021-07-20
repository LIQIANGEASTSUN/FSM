using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEat : StateBase
{
    public StateEat()
    {
        _state = StateEnum.EAT;

        Transition transition = new Transition(StateEnum.RESET, EatToReset);
        _transitionList.Add(transition);
    }

    public override void OnEnter()
    {
        Debug.Log("吃饭开始啦time:"+ Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    {
        // 饥饿感增量
        _player.SenseHunger(-2.5f);
        // 要写作业的强迫性增量
        _player.NeedHomeWork(1.5f);
        // 想打篮球的渴望度增量
        _player.WantBasketball(1);
    }

    public override void OnExit()
    {
        Debug.Log("吃饭结束啦吃的好饱啊打扫厨房time:" + Time.realtimeSinceStartup);
    }

    private bool EatToReset()
    {
        return _player._senseHunger <= 1;
    }
}
