using UnityEngine;

public class EnterTrigger : Trigger
{
    private void OnTriggerEnter(Collider other)
    {
        Invoke(other);
    }
}
