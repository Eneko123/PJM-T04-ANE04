using UnityEngine;

public interface IPositionService
{
    bool IsAvailable { get; }
    void Location();
}
