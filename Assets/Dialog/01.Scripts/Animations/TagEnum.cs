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
        Wobble,     //Parameter: (float speed)
        Shake,      //Parameter: (float power)
        Rainbow,    //None - (May add speed)
        Fromup,     //None - (May add speed)
        Todown,     //None - (May add speed)
        Speed,      //Parameter: (float speed)
        Scale,       //None - (May add duration)
    }

    public enum TMPTag 
    {
        color,
        sup,
        sub,
        mark,
        size,
        b,
        u,
        s,
    }
}
