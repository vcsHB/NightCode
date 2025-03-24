using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public enum TagEnum
    {
        None,
        Wait,       //Parameter: (float time)
        Play,       //Parameter: (string animName)
        Wobble,     //None - (May add speed)
        Shake,      //Parameter: (float power)
        Rainbow,    //None - (May add speed)
        Fromup,     //None - (May add speed)
        Todown      //None - (May add speed)
    }

    public enum TMPTag 
    {
        color,
        sup,
        sub,
        mark,
        b,
        u,
        s,
    }
}
