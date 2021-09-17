using UnityEngine;

public class Rabbit : MonoBehaviour
{

    private static readonly int StartAttack = Animator.StringToHash("StartAttack");

    public Animator Animator;
    public float AttackPeriod = 7;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > AttackPeriod)
        {
            Animator.SetTrigger(StartAttack);
            _timer = 0;
        }
    }
}
