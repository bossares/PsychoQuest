using UnityEngine;

public class TriggerEnter : TriggerBase
{
    private void OnTriggerEnter(Collider other)
    {
        Invoke(other);
    }
}
