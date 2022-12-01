using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_TargetStop : Action
{
    // The speed of the object
   public InputManager_CPU CPU;
    // The transform that the object is moving towards
    public SharedTransform target;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {
        if (CPU.canMove)
        CPU.AxisX = 0;
        
        return TaskStatus.Success;
    }
}