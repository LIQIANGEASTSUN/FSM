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
        Debug.Log("开始吃饭啦:" + _player._senseHunger + "   time:"+ Time.realtimeSinceStartup);
    }

    private int count = 0;
    public override void OnExecute()
    {
        // 吃饭降低饥饿感
        _player.SenseHunger(-2.5f);
        Debug.Log(_player._senseHunger + "    " + Time.realtimeSinceStartup + "   " + ++count + "    " + Time.deltaTime);
    }

    public override void OnExit()
    {
        Debug.Log("吃的好饱啊，不吃了，刷碗、刷锅，擦桌子，打扫厨房:" + _player._senseHunger + "   time:" + Time.realtimeSinceStartup);
    }

    private bool EatToReset()
    {
        return _player._senseHunger <= 1;
    }
}
