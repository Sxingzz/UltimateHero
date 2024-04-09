using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvent : MonoBehaviour
{
    public UnityEvent OnDamageSkill1;

    public void Skill1()
    {
        OnDamageSkill1?.Invoke();
    }
}
