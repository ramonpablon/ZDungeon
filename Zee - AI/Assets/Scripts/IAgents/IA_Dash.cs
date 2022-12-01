using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_Dash : Action {
    public InputManager_CPU CPU;
    // The transform that the object is moving towards
    public SharedTransform target;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {
        if (CPU.canDash && !CPU.isStun)
        {
            CPU.Dasher = true;
        }
        return TaskStatus.Success;

    }
}
