using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicallyTriggerAnimation : MonoBehaviour
{
    public Animator Animator;
    public float WaitAfterTrigger = 3.5f;
    public float Period = 7;
    public string TriggerName = "StartAttack";
    private int TriggerNameHash;
    private float _timer;

    private void Start()
    {
        TriggerNameHash = Animator.StringToHash(TriggerName);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > Period)
        {
            Animator.SetTrigger(TriggerNameHash);
            _timer = -WaitAfterTrigger;
        }
    }
}
