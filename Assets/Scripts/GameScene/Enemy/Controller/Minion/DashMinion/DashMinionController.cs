using UnityEngine;

public class DashMinionController : MinionController
{
    private IState _attackState;

    public IState AttackState => _attackState;

    protected override void Awake()
    {
        base.Awake();

        _normalState = new DashMinionNormalState(this, 3.0f);
        _attackState = new DashMinionAttackState(this, 0.5f);
    }

    public void SetDashSpeed()
    {
        // 방향 설정
        Vector2 dir = DirToTarget();

        // 속도 설정
        _rigidbody.linearVelocity = dir * _model.MoveSpeed * 7.5f;

        // 스프라이트 설정
        _spriteController.SetFlipX(dir.x);
    }

    public void SetMass(float value)
    {
        _rigidbody.mass = 100;
    }
}