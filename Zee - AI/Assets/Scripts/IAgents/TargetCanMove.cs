using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetCanMove : Conditional
{
   // Transform opponentPlayer;
    
   // public bool lol;
    public SharedTransform Target;
    

    public override TaskStatus OnUpdate()
    {

      

        if (Target.Value.transform.GetComponent<PlayerPhisics>().canMove == true)
        {
            return TaskStatus.Success;
        }
        else if (Target.Value.transform.GetComponent<PlayerPhisics>().canMove == false)
        {

            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}