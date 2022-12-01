using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_Attack : Action {
    public InputManager_CPU CPU;
    PlayerPhisics Otherplayer;
    // The transform that the object is moving towards
    public SharedTransform target;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
      //  Otherplayer = target.Value.GetComponent<PlayerPhisics>();
    }
    public override TaskStatus OnUpdate()
    {
        CPU.AxisX = 0;
       // var heading = target.Value.position - this.transform.position;
       // var dot = Vector3.Dot(heading, this.transform.forward);

        if (!CPU.isStun  && !CPU.blocking)
        {


            CPU.Attack = true;
            return TaskStatus.Success;

        }
        //if (dot == -1)
        //{
           // return TaskStatus.Failure;
       // }
        return TaskStatus.Running;
    }

}
