using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
    protected Player _player;

    public PlayerStateBase()
    {

    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public override void OnEnter()
    {

    }

    public override void OnExecute()
    {

    }

    public override void OnExit()
    {

    }
}
