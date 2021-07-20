using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHomeWork : StateBase
{
    public StateHomeWork()
    {
        _state = StateEnum.HOMEWORK;
        SetTransition();
    }

    public override void OnEnter()
    {
        Debug.Log("写作业啦time:" + Time.realtimeSinceStartup);
    }

    public override void OnExecute()
    { 
        // 饥饿感增量
        _player.SenseHunger(0.65f);
        // 作业量增量
        _player.DoHomeWork(-2);
        // 体力增量
        _player.Energy(-1.4f);
    }

    public override void OnExit()
    {
        Debug.Log("写作业停止，作业本收起来time:" + Time.realtimeSinceStartup);
        // 写作业结束，再给一些作业，下次继续写
        _player.DoHomeWork(10);
        // 刚写过作业，将写作业的渴望值设置为 0
        _player.NeedHomeWork(-_player._needHomeWork);
        _player.WantBasketball(1);
    }

    private void SetTransition()
    {
        {
            // 写作业状态到休息状态的转换
            Parameter parameter = new Parameter();
            parameter._parameterName = "Energy";
            parameter.floatValue = 1;
            parameter._parameterType = ParameterType.Float;
            parameter._compare = ParameterCompare.LESS;
            List<Parameter> parameterList = new List<Parameter>() { parameter };
            Transition transition = new Transition(StateEnum.RESET, parameterList);
            TransitionList.Add(transition);
        }

        {
            // 作业写完了到打篮球的状态转换
            Parameter parameter2 = new Parameter();
            parameter2._parameterName = "HomeWorkDone";
            parameter2.floatValue = 1;
            parameter2._parameterType = ParameterType.Float;
            parameter2._compare = ParameterCompare.LESS;
            List<Parameter> parameterList2 = new List<Parameter>() { parameter2 };
            Transition transition2 = new Transition(StateEnum.BASKETBALL, parameterList2);
            TransitionList.Add(transition2);
        }
    }
}
