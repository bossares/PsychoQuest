using UnityEngine;

public class TriggerLeave : TriggerBase
{
    private void OnTriggerExit(Collider other)
    {
        Invoke(other);
    }
}
