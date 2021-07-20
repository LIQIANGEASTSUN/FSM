using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private StateMachine _stateMachine;
    // 饥饿感：饥饿感 >= 80 认为饿了
    public float _senseHunger = 0;
    // 作业量：作业量 <= 0 时，作业写完了
    public float _homeWorkCount = 100;
    // 该写作业的强迫值：值 >= 20 就该写作业了
    public float _needHomeWork = 10;
    // 要去打篮球的渴望值：值 >= 20 就该打篮球了
    public float _wantBasketball = 5;
    // 精力：精力 <= 10 累了
    public float _energy = 100;

    public Player()
    {
        _stateMachine = new StateMachine();
        foreach(var kv in _stateMachine.StateDic)
        {
            kv.Value.SetPlayer(this);
        }
        // 默认开始在休息状态
        _stateMachine.TransitionState(StateEnum.RESET);

        ParameterCompare BehaviorCompare = ParameterCompare.INVALID;
        BehaviorCompare |= ParameterCompare.GREATER;

        Debug.LogError((BehaviorCompare & ParameterCompare.GREATER) != 0);

        // 初始化环境变量
        SenseHunger(0);
        DoHomeWork(0);
        NeedHomeWork(0);
        WantBasketball(0);
        Energy(0);


        {
            // 吃饭状态到休息状态的转换
            //StateBase eat = _stateMachine.StateDic[StateEnum.EAT];
            //Parameter parameter = new Parameter();
            //parameter._parameterName = "Hunger";
            //parameter.boolValue = false;
            //parameter._parameterType = ParameterType.Bool;
            //parameter._compare = ParameterCompare.EQUALS;
            //List<Parameter> parameterList = new List<Parameter>() { parameter };
            //Transition transition = new Transition(StateEnum.RESET, parameterList);
            //eat.TransitionList.Add(transition);
        }

        {
            // 休息状态到吃饭状态的转换
            StateBase reset = _stateMachine.StateDic[StateEnum.RESET];
            Parameter parameter = new Parameter();
            parameter._parameterName = "Hunger";
            parameter.floatValue = 80;
            parameter._parameterType = ParameterType.Float;
            parameter._compare = ParameterCompare.GREATER;
            List<Parameter> parameterList = new List<Parameter>() { parameter };
            Transition transition = new Transition(StateEnum.EAT, parameterList);
            reset.TransitionList.Add(transition);

            return;

            // 休息状态到打篮球状态的转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "WantBasketball";
            parameter2.boolValue = true;
            parameter2._parameterType = ParameterType.Bool;
            parameter2._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2 };
            Transition transition2 = new Transition(StateEnum.BASKETBALL, parameterList2);
            reset.TransitionList.Add(transition2);

            // 休息状态到写作业状态的转换
            Parameter parameter3 = new Parameter();
            parameter3._parameterName = "NeedHomeWork";
            parameter3.boolValue = true;
            parameter3._parameterType = ParameterType.Bool;
            parameter3._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList3 = new List<Parameter>() { parameter3 };
            Transition transition3 = new Transition(StateEnum.HOMEWORK, parameterList3);
            reset.TransitionList.Add(transition3);
        }

        {
            // 写作业状态到休息状态的转换
            StateBase homeWork = _stateMachine.StateDic[StateEnum.HOMEWORK];
            Parameter parameter = new Parameter();
            parameter._parameterName = "Tire";
            parameter.boolValue = true;
            parameter._parameterType = ParameterType.Bool;
            parameter._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList = new List<Parameter>() { parameter };
            Transition transition = new Transition(StateEnum.RESET, parameterList);
            homeWork.TransitionList.Add(transition);

            // 写作业到打篮球的状态转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "WantBasketball";
            parameter2.boolValue = true;
            parameter2._parameterType = ParameterType.Bool;
            parameter2._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2 };
            Transition transition2 = new Transition(StateEnum.BASKETBALL, parameterList2);
            homeWork.TransitionList.Add(transition2);
        }

        {
            // 打篮球状态到吃饭状态的转换
            StateBase basketBall = _stateMachine.StateDic[StateEnum.BASKETBALL];
            Parameter parameter = new Parameter();
            parameter._parameterName = "Hunger";
            parameter.boolValue = true;
            parameter._parameterType = ParameterType.Bool;
            parameter._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList = new List<Parameter>() { parameter };
            Transition transition = new Transition(StateEnum.EAT, parameterList);
            basketBall.TransitionList.Add(transition);

            // 打篮球状态到休息状态的转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "Tire";
            parameter2.boolValue = true;
            parameter2._parameterType = ParameterType.Bool;
            parameter2._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2 };
            Transition transition2 = new Transition(StateEnum.RESET, parameterList2);
            basketBall.TransitionList.Add(transition2);

            // 打篮球到写作业状态的转换
            Parameter parameter3 = new Parameter();
            parameter3._parameterName = "NeedHomeWork";
            parameter3.boolValue = true;
            parameter3._parameterType = ParameterType.Bool;
            parameter3._compare = ParameterCompare.EQUALS;
            List<Parameter> parameterList3 = new List<Parameter>() { parameter3 };
            Transition transition3 = new Transition(StateEnum.HOMEWORK, parameterList3);
            basketBall.TransitionList.Add(transition3);
        }
    }

    private float _interval = 1;
    public void Update()
    {
        //_interval -= Time.deltaTime;
        //if (_interval >= 0)
        //{
        //    return;
        //}
        //_interval = 1;
        _stateMachine.OnExecute();
        
        SenseHunger(0.05f);
        NeedHomeWork(0.2f);
        WantBasketball(0.1f);
        Energy(0.02f);
    }

    // 饥饿感变化，任何时刻都在消耗能量，饥饿感不断上升
    // 吃东西降饥饿感
    public void SenseHunger(float value)
    {
        _senseHunger += value;
        _senseHunger = Mathf.Clamp(_senseHunger, 0, 100);
        _stateMachine.UpdateParameter("Hunger", _senseHunger);
        Debug.LogError(_senseHunger);
    }

    // 做作业
    public void DoHomeWork(float value)
    {
        _homeWorkCount -= value;
        _homeWorkCount = Mathf.Clamp(_homeWorkCount, 0, 100);
        _stateMachine.UpdateParameter("HomeWorkDone", _homeWorkCount);
    }

    public void NeedHomeWork(float value)
    {
        _needHomeWork += value;
        _needHomeWork = Mathf.Clamp(_needHomeWork, 0, 100);
        _stateMachine.UpdateParameter("NeedHomeWork", _needHomeWork);
    }

    public void WantBasketball(float value)
    {
        _wantBasketball += value;
        _wantBasketball = Mathf.Clamp(_wantBasketball, 0, 100);
        _stateMachine.UpdateParameter("WantBasketball", _wantBasketball);
    }

    // 能量，写作业和打篮球会消耗能量，休息增加能量
    public void Energy(float value)
    {
        _energy -= value;
        _wantBasketball = Mathf.Clamp(_energy, 0, 100);
        _stateMachine.UpdateParameter("Tire", _energy);
    }

}
