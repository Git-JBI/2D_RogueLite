using System;
using UnityEngine;

public class EnergyManager
{
    private EnergyModel _energyModel;

    public EnergyManager(EnergyModel energyModel)
    {
        _energyModel = energyModel;

        CalculateOffLineTime();
    }

    /// <summary>
    /// 
    /// </summary>
    public void CalculateOffLineTime()
    {
        long saveTime = Convert.ToInt64(_energyModel.LastLoginTime);
        TimeSpan time = DateTime.Now - DateTime.FromBinary(saveTime);

        Debug.Log(time.TotalSeconds);
    }
    #region Pause & Quit
    public void OnApplicationFocus(bool focus)
    {
        Debug.Log($"Focus : {focus}");
    }
    public void OnApplicationPause(bool pause)
    {
        Debug.Log("Pause");
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }
    #endregion
}
