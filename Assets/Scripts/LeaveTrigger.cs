using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class LeaveTrigger : MonoBehaviour
{
    public event UnityAction<Collider> Leaved;

    private void OnTriggerExit(Collider other)
    {
        Leaved?.Invoke(other);
    }
}
