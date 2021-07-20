using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBasketball : StateBase
{
    public StateBasketball()
    {
        _state = StateEnum.BASKETBALL;
    }

    public override void OnEnter()
    {
        Debug.Log("开始打篮球啦，好高兴啊");
    }

    public override void OnExecute()
    {
        // 每帧渴望打篮球的需求降低 1
        _player.WantBasketball(-1);
        // 每帧体力不断减少 0.3f
        _player.Energy(0.3f);
        // 每帧饥饿感增加 0.2f
        _player.SenseHunger(0.2f);
    }

    public override void OnExit()
    {
        Debug.Log("停止打篮球");
    }
}
