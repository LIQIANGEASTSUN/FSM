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
        // 设置这个人的有限状态机配置文件
        _stateMachine.SetConfigFile("FSM");

        // 初始化环境变量
        SenseHunger(0);
        DoHomeWork(0);
        NeedHomeWork(0);
        WantBasketball(0);
        Energy(0);
    }

    public void Update()
    {
        _stateMachine.OnExecute();
    }

    // 饥饿感变化，任何时刻都在消耗能量，饥饿感不断上升
    // 吃东西降饥饿感
    public void SenseHunger(float value)
    {
        _senseHunger = Mathf.Clamp(_senseHunger + value * Time.deltaTime, 0, 10);
        _stateMachine.UpdateParameter("Hunger", _senseHunger);
    }

    // 作业量增量
    public void DoHomeWork(float value)
    {
        _homeWorkCount = Mathf.Clamp(_homeWorkCount + value * Time.deltaTime, 0, 10); ;
        _stateMachine.UpdateParameter("HomeWorkDone", _homeWorkCount);
    }

    // 写作业的强迫性增量
    public void NeedHomeWork(float value)
    {
        _needHomeWork = Mathf.Clamp(_needHomeWork + value * Time.deltaTime, 0, 10);
        _stateMachine.UpdateParameter("NeedHomeWork", _needHomeWork);
    }

    // 打篮球渴望度增量
    public void WantBasketball(float value)
    {
        _wantBasketball = Mathf.Clamp(_wantBasketball + value * Time.deltaTime, 0, 10);
        _stateMachine.UpdateParameter("WantBasketball", _wantBasketball);
    }

    // 精力增量
    public void Energy(float value)
    {
        _energy = Mathf.Clamp(_energy + value * Time.deltaTime, 0, 10);
        _stateMachine.UpdateParameter("Energy", _energy);
    }

    private Dictionary<StateEnum, Vector3> _positionDic = new Dictionary<StateEnum, Vector3>();
    private Transform _player;
    public bool MoveTo(StateEnum stateEnum)
    {
        if (null == _player)
        {
            GameObject p = GameObject.Find("Player");
            _player = p.transform;
        }

        Vector3 position;
        if (!_positionDic.TryGetValue(stateEnum, out position))
        {
            if (stateEnum == StateEnum.EAT)
            {
                position = GameObject.Find("Eat").transform.position;
            }
            else if (stateEnum == StateEnum.RESET)
            {
                position = GameObject.Find("Reset").transform.position;
            }
            else if (stateEnum == StateEnum.BASKETBALL)
            {
                position = GameObject.Find("Basketball").transform.position;
            }
            else if (stateEnum == StateEnum.HOMEWORK)
            {
                position = GameObject.Find("HomeWork").transform.position;
            }
        }

        if (Vector3.Distance(_player.position, position) <= 0.5f)
        {
            return true;
        }

        _player.Translate((position - _player.position).normalized * Time.deltaTime * 2, Space.World);
        return Vector3.Distance(_player.position, position) <= 0.5f;
    }
}
