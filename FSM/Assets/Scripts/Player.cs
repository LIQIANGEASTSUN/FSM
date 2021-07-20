using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private StateMachine _stateMachine;
    // 饥饿感：饥饿感 >= 8 认为饿了
    public float _senseHunger = 0;
    // 作业量：作业量 <= 0 时，作业写完了
    public float _homeWorkCount = 10;
    // 该写作业的强迫值：值 >= 10 就该写作业了
    public float _needHomeWork = 0;
    // 要去打篮球的渴望值：值 >= 10 就该打篮球了
    public float _wantBasketball = 0;
    // 精力：精力 <= 1 累了
    public float _energy = 10;

    public Player()
    {
        _stateMachine = new StateMachine();
        foreach(var kv in _stateMachine.StateDic)
        {
            kv.Value.SetPlayer(this);
        }
        // 默认开始在休息状态
        _stateMachine.TransitionState(StateEnum.RESET);

        // 初始化环境变量
        SenseHunger(0);
        DoHomeWork(0);
        NeedHomeWork(0);
        WantBasketball(0);
        Energy(0);
    }

    private float _interval = 1;
    public void Update()
    {
        _interval -= Time.deltaTime;
        if (_interval >= 0)
        {
            return;
        }
        _interval = 1;
        _stateMachine.OnExecute();
    }

    // 饥饿感变化，任何时刻都在消耗能量，饥饿感不断上升
    // 吃东西降饥饿感
    public void SenseHunger(float value)
    {
        _senseHunger += value;
        _senseHunger = Mathf.Clamp(_senseHunger, 0, 10);
        _stateMachine.UpdateParameter("Hunger", _senseHunger);
        //Debug.LogError(_senseHunger);
    }

    // 做作业
    public void DoHomeWork(float value)
    {
        _homeWorkCount += value;
        _homeWorkCount = Mathf.Clamp(_homeWorkCount, 0, 10);
        _stateMachine.UpdateParameter("HomeWorkDone", _homeWorkCount);
    }

    public void NeedHomeWork(float value)
    {
        _needHomeWork += value;
        _needHomeWork = Mathf.Clamp(_needHomeWork, 0, 10);
        _stateMachine.UpdateParameter("NeedHomeWork", _needHomeWork);
    }

    public void WantBasketball(float value)
    {
        _wantBasketball += value;
        _wantBasketball = Mathf.Clamp(_wantBasketball, 0, 10);
        _stateMachine.UpdateParameter("WantBasketball", _wantBasketball);
    }

    // 能量，写作业和打篮球会消耗能量，休息增加能量
    public void Energy(float value)
    {
        _energy += value;
        _energy = Mathf.Clamp(_energy, 0, 100);
        _stateMachine.UpdateParameter("Energy", _energy);
    }

}
