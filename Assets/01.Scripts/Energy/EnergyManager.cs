using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnergyManager : MonoBehaviour
{
    private EnergyModel _energyModel;

    private int _remainEnergy;

    private Coroutine _updateRemainTimeCoroutine;

    public event UnityAction<int, int> OnEnergyChanged;
    public event UnityAction<int> OnTimeChanged;

    public int Energy => _energyModel.Energy;
    public int MaxEnergy => _energyModel.MaxEnergy;
    public int EnergyRechargeRemainingTime => _energyModel.EnergyRechargeRemainingTime;

    public void Init(EnergyModel model, float saveTime)
    {
        _energyModel = model;

        // 에너지가 가득 찬 상태가 아닌 경우
        if(!_energyModel.IsEnergyFull)
        {
            CalculateOffLineTime();
        }
    }
    /// <summary>
    /// 에너지를 사용하는 함수
    /// </summary>
    /// <param name="reduce"></param>
    public void UseEnergy(int reduce)
    {
        // 남은 시간 설정
        _energyModel.AddEnergyRechargeRemainingTime(reduce * 600);

        // 에너지 감소
        _energyModel.AddEnergy(-reduce);

        // 남은 에너지 설정
        _remainEnergy = reduce;

        // 현재 진행중인 코루틴이 없을 경우
        if(_updateRemainTimeCoroutine == null)
        {
            // 남은시간 코루틴 실행
            _updateRemainTimeCoroutine = StartCoroutine(UpdateRemainTime());
        }
    }

    /// <summary>
    /// 오프라인 시간 계산
    /// </summary>
    private void CalculateOffLineTime()
    {
        // 현재 시간 - 최근 접속시간을 초로 나타낸 값
        int distanceSeconds = (int)(DateTime.Now - DateTime.FromBinary(Convert.ToInt64(_energyModel.LastLoginTime))).TotalSeconds;

        // 시간 차가 에너지 재 충전까지 남은 시간보다 크거나 같은 경우
        if (distanceSeconds > _energyModel.EnergyRechargeRemainingTime)
        {
            // 남은 시간에 따라 충전 될 에너지
            int energy = (_energyModel.EnergyRechargeRemainingTime / 600) + 1;

            // 남은 시간 설정(시간 차 만큼 감소)
            _energyModel.AddEnergyRechargeRemainingTime(-distanceSeconds);

            // 에너지 추가
            _energyModel.AddEnergy(energy);
        }

        // 시간 차가 에너지 재 충전까지 남은 시간보다 작은 경우
        else
        {
            // 시간 차에 따라 충전 될 에너지
            int energy = _energyModel.EnergyRechargeRemainingTime / 600 - ((_energyModel.EnergyRechargeRemainingTime - distanceSeconds) / 600);

            // 남은 시간 갱신
            _energyModel.AddEnergyRechargeRemainingTime(-distanceSeconds);

            // 충전할 에너지가 0 초과일 경우
            if (energy > 0)
                // 에너지 추가
                _energyModel.AddEnergy(energy);

            // 남은 에너지 값 설정
            _remainEnergy = _energyModel.RemainChargeEnergy;

            // 남은 시간 업데이트용 코루틴 실행
            _updateRemainTimeCoroutine = StartCoroutine(UpdateRemainTime());
        }
    }
    /// <summary>
    /// 최근 접속 시간을 설정하는 함수
    /// </summary>
    private void SaveLastLoginTime()
    {
        string now = DateTime.Now.ToBinary().ToString();

        _energyModel.SaveLastLoginTime(now);
    }

    /// <summary>
    /// 에너지가 가득 찬 상태가 아닐 경우 시간을 업데이트할 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateRemainTime()
    {
        // 에너지가 가득 찬 상태가 아닐 경우
        while(!_energyModel.IsEnergyFull)
        {
            // 남은 시간 1초 감소
            _energyModel.AddEnergyRechargeRemainingTime(-1);

            // 최근 접속 시간 설정
            SaveLastLoginTime();

            // 시간 바뀜 이벤트 실행
            OnTimeChanged?.Invoke(_energyModel.EnergyRechargeRemainingTime);

            // 남은 시간 상 에너지 충전이 될 때
            if(_remainEnergy > (_energyModel.EnergyRechargeRemainingTime / 600) + 1)
            {
                // 에너지 충전
                _energyModel.AddEnergy();

                // 남은 에너지 값 설정
                _remainEnergy = _energyModel.RemainChargeEnergy;

                // 에너지 변경 이벤트 실행
                OnEnergyChanged?.Invoke(_energyModel.Energy, _energyModel.MaxEnergy);
            }

            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
