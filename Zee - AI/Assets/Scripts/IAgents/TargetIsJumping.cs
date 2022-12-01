using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetIsJumping : Conditional
{
    public SharedTransform Target;
    Rigidbody TargetRb;
    public override void OnAwake()
    {
        TargetRb = Target.Value.transform.gameObject.GetComponent<Rigidbody>();

    }

    public override TaskStatus OnUpdate()
    {


        if (TargetRb.velocity.y >= 0.1f || TargetRb.velocity.y <= -0.01)
        {
            return TaskStatus.Success;
        }
        else
        {

            return TaskStatus.Failure;
        }
    }
}