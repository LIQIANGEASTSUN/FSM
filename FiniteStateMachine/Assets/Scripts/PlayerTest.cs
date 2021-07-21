using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = new Player();
    }

    // Update is called once per frame
    void Update()
    {
        _player.Update();
    }
}
