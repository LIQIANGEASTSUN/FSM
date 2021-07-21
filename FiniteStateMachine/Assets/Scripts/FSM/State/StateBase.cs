﻿using System.Collections.Generic;
using System;

public abstract class StateBase
{
    protected Player _player;
    // 当前类型
    protected StateEnum _state;
    protected List<Transition> _transitionList = new List<Transition>();

    public StateBase(){
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    // 进入该状态
    public abstract void OnEnter();

    // 执行该状态的行为
    public abstract void OnExecute();

    // 退出该状态
    public abstract void OnExit();

    //返回当前类型
    public StateEnum State
    {
        get { return _state; }
    }

    public List<Transition> TransitionList
    {
        get { return _transitionList; }
    }
}
