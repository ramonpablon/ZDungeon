using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_Target_Attackdir : Action
{
    // The speed of the object
   public InputManager_CPU CPU;
    // The transform that the object is moving towards
    public SharedTransform target;
    public int direction;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {

        CPU.AxisY = direction;
        
        return TaskStatus.Success;
    }
}