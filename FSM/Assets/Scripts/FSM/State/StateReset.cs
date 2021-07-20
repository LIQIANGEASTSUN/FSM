using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReset : StateBase
{
    public StateReset()
    {
        _state = StateEnum.RESET;
        SetTransition();
    }

    public override void OnEnter()
    {
        Debug.Log("休息开始了time:" + Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    {
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

    private void SetTransition()
    {
        Parameter parameterEnergy = new Parameter();
        parameterEnergy._parameterName = "Energy";
        parameterEnergy.floatValue = 9.5f;
        parameterEnergy._parameterType = ParameterType.Float;
        parameterEnergy._compare = ParameterCompare.GREATER;

        {
            // 休息状态到吃饭状态的转换
            Parameter parameter = new Parameter();
            parameter._parameterName = "Hunger";
            parameter.floatValue = 8;
            parameter._parameterType = ParameterType.Float;
            parameter._compare = ParameterCompare.GREATER;
            List<Parameter> parameterList = new List<Parameter>() { parameter, parameterEnergy };
            Transition transition = new Transition(StateEnum.EAT, parameterList);
            TransitionList.Add(transition);
        }

        {
            // 休息状态到打篮球状态的转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "WantBasketball";
            parameter2.floatValue = 8;
            parameter2._parameterType = ParameterType.Float;
            parameter2._compare = ParameterCompare.GREATER;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2, parameterEnergy };
            Transition transition2 = new Transition(StateEnum.BASKETBALL, parameterList2);
            TransitionList.Add(transition2);
        }

        {
            // 休息状态到写作业状态的转换
            Parameter parameter4 = new Parameter();
            parameter4._parameterName = "NeedHomeWork";
            parameter4.floatValue = 8;
            parameter4._parameterType = ParameterType.Float;
            parameter4._compare = ParameterCompare.GREATER;
            List<Parameter> parameterList4 = new List<Parameter>() { parameter4, parameterEnergy };
            Transition transition4 = new Transition(StateEnum.HOMEWORK, parameterList4);
            TransitionList.Add(transition4);
        }
    }
}
