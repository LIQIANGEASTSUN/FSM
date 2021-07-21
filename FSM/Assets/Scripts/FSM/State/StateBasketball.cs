using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBasketball : StateBase
{
    public StateBasketball()
    {
        _state = StateEnum.BASKETBALL;
        SetTransition();
    }

    public override void OnEnter()
    {
        Debug.Log("打篮球开始啦，好高兴啊time:" + Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    {
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

    private void SetTransition()
    {
        {
            // 打篮球状态到吃饭状态的转换
            Parameter parameter = new Parameter();
            parameter._parameterName = "Hunger";
            parameter.floatValue = 8;
            parameter._parameterType = ParameterType.Float;
            parameter._compare = ParameterCompare.GREATER;
            List<Parameter> parameterList = new List<Parameter>() { parameter };
            Transition transition = new Transition(StateEnum.EAT, parameterList);
            TransitionList.Add(transition);
        }

        {
            // 打篮球状态到休息状态的转换
            Parameter parameter1 = new Parameter();
            parameter1._parameterName = "Energy";
            parameter1.floatValue = 1;
            parameter1._parameterType = ParameterType.Float;
            parameter1._compare = ParameterCompare.LESS;
            List<Parameter> parameterList1 = new List<Parameter>() { parameter1 };
            Transition transition1 = new Transition(StateEnum.RESET, parameterList1);
            TransitionList.Add(transition1);
        }

        {
            // 打篮球到写作业状态的转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "NeedHomeWork";
            parameter2.floatValue = 9;
            parameter2._parameterType = ParameterType.Float;
            parameter2._compare = ParameterCompare.GREATER;

            Parameter parameter3 = new Parameter();
            parameter3._parameterName = "WantBasketball";
            parameter3.floatValue = 1;
            parameter3._parameterType = ParameterType.Float;
            parameter3._compare = ParameterCompare.LESS;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2, parameter3 };
            Transition transition2 = new Transition(StateEnum.BASKETBALL, parameterList2);
            TransitionList.Add(transition2);
        }
    }
}
