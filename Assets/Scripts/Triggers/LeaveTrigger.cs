using UnityEngine;

public class LeaveTrigger : Trigger
{
    private void OnTriggerExit(Collider other)
    {
        Invoke(other);
    }
}
