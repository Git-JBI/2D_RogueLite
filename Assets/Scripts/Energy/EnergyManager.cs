using System;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private EnergyModel _energyModel;

    public void Init(EnergyModel model)
    {
        _energyModel = model;

        CalculateOffLineTime();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CalculateOffLineTime()
    {
        long saveTime = Convert.ToInt64(_energyModel.LastLoginTime);
        TimeSpan time = DateTime.Now - DateTime.FromBinary(saveTime);

        Debug.Log(time.TotalSeconds);
    }

    private void SaveLastLoginTime()
    {
        string now = DateTime.Now.ToBinary().ToString();

        _energyModel.SaveLastLoginTime(now);
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
