using System;
using System.Collections;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private EnergyModel _energyModel;

    public void Init(EnergyModel model, float saveTime)
    {
        _energyModel = model;

        CalculateOffLineTime();

        StartCoroutine(SaveTimeRoutine(saveTime));
    }

    /// <summary>
    /// 현재 에너지를 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public int GetEnergy()
    {
        return _energyModel.Energy;
    }
    /// <summary>
    /// 에너지를 사용하는 함수
    /// </summary>
    /// <param name="reduce"></param>
    public void UseEnergy(int reduce)
    {
        _energyModel.IncreaseEnergy(-reduce);
    }

    /// <summary>
    /// 오프라인 시간 계산
    /// </summary>
    private void CalculateOffLineTime()
    {
        // 최근 접속 시간
        long saveTime = Convert.ToInt64(_energyModel.LastLoginTime);

        // 현재 시간 - 최근 접속 시간
        TimeSpan time = DateTime.Now - DateTime.FromBinary(saveTime);

        // 10분에 1씩 차도록 하는 에너지
        int energy = (int)time.TotalSeconds / 600;

        // 에너지 추가
        _energyModel.IncreaseEnergy(energy);
    }
    /// <summary>
    /// 최근 접속 시간을 갱신하는 함수
    /// </summary>
    private void SaveLastLoginTime()
    {
        string now = DateTime.Now.ToBinary().ToString();

        _energyModel.SaveLastLoginTime(now);
    }
    /// <summary>
    /// 정해진 시간마다 최근 접속 시간을 저장하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator SaveTimeRoutine(float time)
    {
        SaveLastLoginTime();

        yield return new WaitForSeconds(time);
    }
    #region Pause & Quit
    public void OnApplicationPause(bool pause)
    {
        if (_energyModel != null)
            SaveLastLoginTime();
    }
    public void OnApplicationQuit()
    {
        if (_energyModel != null)
            SaveLastLoginTime();
    }
    #endregion
}
