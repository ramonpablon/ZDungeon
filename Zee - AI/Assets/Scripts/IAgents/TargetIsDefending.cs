using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetIsDefending : Conditional
{
   // Transform opponentPlayer;
    
   // public bool lol;
    public SharedTransform Target;
    public override void OnAwake()
    {
    }

    public override TaskStatus OnUpdate()
    {

      

        if (Target.Value.transform.GetComponent<PlayerPhisics>().blocking == false)
        {
            return TaskStatus.Failure;
        }
        else if (Target.Value.transform.GetComponent<PlayerPhisics>().blocking == true)
        {

            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}