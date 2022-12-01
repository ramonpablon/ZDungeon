using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetIsClose : Conditional
{
   // Transform opponentPlayer;
    
    public float MaxDistance = 2.7f;
    private float distance;
    // public bool lol;
    public SharedTransform Target;
    public override void OnAwake()
    {
    }

    public override TaskStatus OnUpdate()
    {

       distance = Vector3.Distance(this.transform.position, Target.Value.transform.position);
        //Debug.Log(distance);


        if (distance > MaxDistance)
        {
            return TaskStatus.Failure;

        }
        else if (distance <= MaxDistance)
        {

            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}