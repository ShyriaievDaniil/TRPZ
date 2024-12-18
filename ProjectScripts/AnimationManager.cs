using System.Collections.Generic;
using UnityEngine;

public class AnimationManager: MonoBehaviour
{
    private Dictionary<object, Animator> _animators = new Dictionary<object, Animator>();

    public void Awake()
    {
        EventBus.Walking += OnWalking;
        EventBus.Attacking += OnAttacking;
        EventBus.Falling += OnFalling;
    }
    public void OnWalking(object obj, bool isWalking)
    {
        findAnimator(obj).SetBool("IsWalk", isWalking);
    }
    public void OnAttacking(object obj)
    {
        findAnimator(obj).SetTrigger("Attack");
    }
    public void OnFalling(object obj, bool isFalling)
    {
        findAnimator(obj).SetBool("IsFalling", isFalling);
    }
    private Animator findAnimator(object obj)
    {
        if (_animators.ContainsKey(obj))
        {
            return _animators[obj];
        }
        else
        {
            Component component = (Component)obj;
            Animator animator = component.gameObject.GetComponent<Animator>();
            _animators.Add(obj, animator);
            return animator;
        }
    }
}
