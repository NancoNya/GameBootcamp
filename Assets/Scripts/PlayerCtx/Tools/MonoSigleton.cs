using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSigleton<S> : MonoBehaviour where S : MonoBehaviour
{
    public bool global;
    private static S instance;
    private readonly static object _lock = new object();

    public static S Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType(typeof(S)) as S;
                    }
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if (global)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        instance = this as S;
    }
}
