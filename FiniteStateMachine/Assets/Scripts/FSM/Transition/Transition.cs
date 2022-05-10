using System.Collections.Generic;
using GraphicTree;

public delegate bool TransitionCallback();
/// <summary>
/// 状态装换条件
/// </summary>
public class Transition
{
    // 切换状态事件
    private TransitionCallback _transitionCallback;
    private List<NodeParameter> _parameterList = new List<NodeParameter>();

    public Transition(StateEnum toState, TransitionCallback transitionCallback)
    {
        ToState = toState;
        _transitionCallback = transitionCallback;
    }

    public Transition(StateEnum toState, List<NodeParameter> parameterList)
    {
        ToState = toState;
        _parameterList = parameterList;
    }

    public StateEnum ToState { get; set; }

    public List<NodeParameter> ParameterList
    {
        get { return _parameterList; }
    }

    public bool CanTransition()
    {
        if (null == _transitionCallback)
        {
            return false;
        }
        return _transitionCallback();
    }
}