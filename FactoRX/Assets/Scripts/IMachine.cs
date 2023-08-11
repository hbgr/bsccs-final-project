using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachine
{
    void Activate();
    Transform Transform { get; }
    void SetArenaController(ArenaController arenaController);
}
