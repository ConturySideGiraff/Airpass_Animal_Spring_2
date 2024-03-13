using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class FsmSingleton<TE> : Singleton<FsmSingleton<TE>> where TE : Enum
{
    [SerializeField] private TE current;

    public TE Current => current;

    private readonly Dictionary<TE, Action> _fsmDic = new Dictionary<TE, Action>();
    
    protected virtual void Init()
    {
        var infos = from info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            where info.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Length == 0
                  && info.Name.Split('_')[0] == "Fsm"
            select info;

        _fsmDic.Clear();
        foreach (var info in infos)
        {
            Enum.TryParse(typeof(TE), info.Name.Split('_')[1], true, out object @enum);
            _fsmDic.Add((TE)@enum, Delegate.CreateDelegate(typeof(Action), this, info) as Action);
        }
    }

    public virtual void Change(Component sender,  TE e)
    {
        if (!_fsmDic.ContainsKey(e))
        {
            Debug.LogError($"Not Change, [ private void Fsm_{e} ] Is Not Exist");
            return;
        }

        _fsmDic[e]?.Invoke();
        
        current = e;
    }
}
