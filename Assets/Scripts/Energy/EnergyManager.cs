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
        Debug.Log(_energyModel.LastLoginTime);
        long saveTime = Convert.ToInt64(_energyModel.LastLoginTime);
        TimeSpan time = DateTime.Now - DateTime.FromBinary(saveTime);

        Debug.Log(time.TotalSeconds);
    }

    private void SaveLastLoginTime()
    {
        Debug.Log("SaveLastLoginTime");
    }

    #region Pause & Quit
    public void OnApplicationFocus(bool focus)
    {
        SaveLastLoginTime();
    }
    public void OnApplicationPause(bool pause)
    {
        SaveLastLoginTime();
    }
    public void OnApplicationQuit()
    {
        SaveLastLoginTime();
    }
    #endregion
}
