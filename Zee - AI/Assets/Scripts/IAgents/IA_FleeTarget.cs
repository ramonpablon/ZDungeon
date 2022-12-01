using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_FleeTarget : Action
{
    // The speed of the object
    public InputManager_CPU IM;
    // The transform that the object is moving towards
    public SharedTransform target;
    public override void OnAwake()
    {
        IM = this.transform.GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {

        var relativePoint = transform.InverseTransformPoint(target.Value.transform.position);
        if (relativePoint.x < 0.0)
        {
            IM.AxisX = 1;

        }
        else if (relativePoint.x > 0.0)
        {//

            IM.AxisX = -1;
        }
        
        return TaskStatus.Success;
    }
}