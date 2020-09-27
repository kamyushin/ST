using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public enum ExecutionOrder
    {
        SYSTEM,
        DEFAULT = 100,
        INPUT,
        PLAYER,
        CAMERA,
        POST,
    }
}
