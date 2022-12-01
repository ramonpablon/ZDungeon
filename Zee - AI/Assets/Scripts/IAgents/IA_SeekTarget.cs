using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_SeekTarget : Action
{
    // The speed of the object
    public InputManager_CPU IM;
    // The transform that the object is moving towards
    public SharedTransform target;
    bool shouldmove = false;
    public override void OnAwake()

    {
        IM = this.transform.GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {
        shouldmove = true;
        var relativePoint = transform.InverseTransformPoint(target.Value.transform.position);
        if (relativePoint.x < 0.0 && IM.canMove && !IM.isStun )
        {

            IM.AxisX = -1;

        }
        if (relativePoint.x > 0.0 && IM.canMove && !IM.isStun)
        {//

            IM.AxisX = 1;
        }
        
       
    


        
        return TaskStatus.Success;
    }
   public override void OnEnd() {

        shouldmove = false;
    }

}