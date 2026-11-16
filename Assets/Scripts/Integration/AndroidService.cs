using UnityEngine;

public class AndroidService : IPositionService
{
    public bool IsAvailable => SystemInfo.supportsLocationService;

    public void Location()
    {
        if (IsAvailable) Handheld.GetActivityIndicatorStyle();
    }
}
