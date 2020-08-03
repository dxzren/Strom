using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace StormBattle
{
    public class BattleRoot : ContextView
    {
        private void Awake()
        {
            context     = new BattleContext(this);
        }

    }
}
