using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIAnimInterface
{
    public void AnimStart(Action endAction );

    public void AnimStop();
}
