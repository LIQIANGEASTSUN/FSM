using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateEnum
{
    EAT = 0,        // 吃饭

    RESET = 1,      // 休息

    BASKETBALL = 2, // 休息

    HOMEWORK = 3,   // 写作业
}

public class StateMachine
{
    // 保存所有的状态
    private Dictionary<StateEnum, StateBase> _stateDic = new Dictionary<StateEnum, StateBase>();
    // 记录当前状态
    private StateBase _currentState;
    // 环境变量
    private Dictionary<string, Parameter> _parameterDic = new Dictionary<string, Parameter>();

    public StateMachine()
    {
        // 初始化状态、并存储
        _stateDic[StateEnum.EAT] = new StateEat();
        _stateDic[StateEnum.RESET] = new StateReset();
        _stateDic[StateEnum.BASKETBALL] = new StateBasketball();
        _stateDic[StateEnum.HOMEWORK] = new StateHomeWork();
    }

    public Dictionary<StateEnum, StateBase> StateDic
    {
        get { return _stateDic; }
    }

    // 获取当前状态
    public StateBase CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }

    // 状态转换方法
    public void TransitionState(StateEnum stateEnum)
    {
        // 如果当前状态不为空，先退出当前状态
        if (null != CurrentState)
        {
            CurrentState.OnExit();
        }

        // 令当前状态等于转换的新状态
        CurrentState = _stateDic[stateEnum];
        // 转换的新状态执行 进入方法
        CurrentState.OnEnter();
        CurrentState.OnExecute();
    }

    // 每帧执行的方法
    public void OnExecute()
    {
        if (null != CurrentState)
        {
            CurrentState.OnExecute();
        }

        // 判断所有转换条件
        for (int i = 0; i < CurrentState.TransitionList.Count; ++i)
        {
            Transition transition = CurrentState.TransitionList[i];
            // 如果转换条件为 true，则转换状态
            if (transition.CanTransition() || CompareParameter(transition))
            {
                if (transition.ToState == StateEnum.RESET)
                {
                    int a = 0;
                    bool result = transition.CanTransition();
                    CompareParameter(transition);
                }
                TransitionState(transition.ToState);
                break;
            }
        }
    }

    private bool CompareParameter(Transition transition)
    {
        if (transition.ParameterList.Count <= 0)
        {
            return false;
        }
        for (int i = 0; i < transition.ParameterList.Count; ++i)
        {
            Parameter parameter = transition.ParameterList[i];
            Parameter environment = null;
            if (!_parameterDic.TryGetValue(parameter._parameterName, out environment)
                || !ConditionCompare.CompareParameter(environment, parameter))
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateParameter(string parameterName, int intValue)
    {
        Parameter parameter = GetParameter(parameterName);
        parameter._parameterType = ParameterType.Int;
        parameter.intValue = intValue;
    }

    public void UpdateParameter(string parameterName, float floatValue)
    {
        Parameter parameter = GetParameter(parameterName);
        parameter._parameterType = ParameterType.Float;
        parameter.floatValue = floatValue;
    }

    public void UpdateParameter(string parameterName, bool boolValue)
    {
        Parameter parameter = GetParameter(parameterName);
        parameter._parameterType = ParameterType.Bool;
        parameter.boolValue = boolValue;
    }

    public void UpdateParameter(string parameterName, string stringValue)
    {
        Parameter parameter = GetParameter(parameterName);
        parameter._parameterType = ParameterType.String;
        parameter.stringValue = stringValue;
    }

    private Parameter GetParameter(string parameterName)
    {
        Parameter parameter = null;
        if (!_parameterDic.TryGetValue(parameterName, out parameter))
        {
            parameter = new Parameter();
            _parameterDic[parameterName] = parameter;
        }
        return parameter;
    }
}
