using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetIsAttacking : Conditional
{
   // Transform opponentPlayer;
    
   // public bool lol;
    public SharedTransform Target;
    public override void OnAwake()
    {
    }

    public override TaskStatus OnUpdate()
    {

      

        if (Target.Value.transform.GetComponent<PlayerPhisics>().inCombat == false)
        {
            return TaskStatus.Failure;
        }
        else if (Target.Value.transform.GetComponent<PlayerPhisics>().inCombat == true)
        {

            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}