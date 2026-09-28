using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowingStar : WeaponControllerBase
{
    //TODO ResourceLoad로 변경
    [SerializeField]private PlayerWeaponObjectBase _throwingStar;

    /// <summary>
    /// 삽 갯수
    /// </summary>
    private float _bulletCount;
    /// <summary>
    /// 회전 속도
    /// </summary>
    private float _rotSpeed;
    /// <summary>
    /// 캐릭터와 삽 사이의 반지름
    /// </summary>
    private float _radius;
    /// <summary>
    /// 쿨타임
    /// </summary>
    private float _coolTime;
    /// <summary>
    /// 지속 시간
    /// </summary>
    private float _duration;

    private float _timer;
    private float _coolingTimer;
    private bool _isCooling;

    private List<PlayerWeaponObjectBase> _throwingStars = new();

    private void Update()
    {
        CheckCoolTime();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }

    private void CheckCoolTime()
    {
        // 지속시간 이상이 되었을 시
        if(_timer >= _duration)
        {
            DisableStars();
            _isCooling = true;
            _timer = 0;
        }
        // 쿨타임이 끝났을 시
        if(_coolingTimer >= _coolTime)
        {
            EnableStars();
            _isCooling = false;
            _coolingTimer = 0;
        }

        if (!_isCooling)
            _timer += Time.deltaTime;
        else
            _coolingTimer += Time.deltaTime;
    }

    public override void Init(InGameHeroModel model)
    {
        base.Init(model);
        _radius = 3;
        _timer = 0;
        _coolingTimer = 0;
        _isCooling = false;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        RemoveBullets();

        SpawnBullets();
    }
    
    /// <summary>
    /// 삽 켜기
    /// </summary>
    private void EnableStars()
    {
        foreach(PlayerWeaponObjectBase x in _throwingStars)
        {
            x.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 표창 끄기
    /// </summary>
    private void DisableStars()
    {
        foreach (PlayerWeaponObjectBase x in _throwingStars)
        {
            x.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 표창 소환
    /// </summary>
    private void SpawnBullets()
    {
        float angle = 360 / _bulletCount;

        for(int i = 0; i < _bulletCount; i++)
        {
            PlayerWeaponObjectBase throwigStar = Instantiate(_throwingStar);

            throwigStar.Init(_heroModel, _damageMagnification);

            Vector2 dir = new Vector2(Mathf.Cos(i * angle * Mathf.Deg2Rad), Mathf.Sin(i * angle * Mathf.Deg2Rad));

            throwigStar.transform.up = dir;

            throwigStar.transform.SetParent(transform, false);

            throwigStar.transform.localPosition = dir * _radius;

            _throwingStars.Add(throwigStar);
        }
    }

    /// <summary>
    /// 삽 제거
    /// </summary>
    private void RemoveBullets()
    {
        foreach(PlayerWeaponObjectBase bullet in _throwingStars)
        {
            Destroy(bullet.gameObject);
        }
        _throwingStars.Clear();
    }

    #region abstract
    protected override void CalculateOtherStats()
    {
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level);
        _rotSpeed = _weaponData.GetStatValue(WeaponStatType.RotSpeed, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
        _duration = _weaponData.GetStatValue(WeaponStatType.Duration, _level);
    }
    #endregion
}