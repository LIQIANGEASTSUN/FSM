using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBasketball : PlayerStateBase
{
    public StateBasketball()
    {
        _state = StateEnum.BASKETBALL;
    }

    public override void OnEnter()
    {
        Debug.Log("打篮球开始啦，好高兴啊time:" + Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    {
        if (!_player.MoveTo(StateEnum.BASKETBALL))
        {
            return;
        }
        // 饥饿感增量
        _player.SenseHunger(0.7f);
        // 要写作业的强迫性增量
        _player.NeedHomeWork(1.5f);
        // 想打篮球的渴望度增量
        _player.WantBasketball(-1);
        // 体力增量
        _player.Energy(-1.5f);
    }

    public override void OnExit()
    {
        Debug.Log("打篮球停止time:" + Time.realtimeSinceStartup);
        // 刚打过篮球，将打篮球的渴望值设置为0
        _player.WantBasketball(-_player._wantBasketball);
    }
}
