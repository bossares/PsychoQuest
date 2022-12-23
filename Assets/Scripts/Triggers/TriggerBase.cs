using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public abstract class TriggerBase : MonoBehaviour
{
    public event UnityAction<Collider> Involved;

    protected void Invoke(Collider other)
    {
        Involved?.Invoke(other);
    }
}
