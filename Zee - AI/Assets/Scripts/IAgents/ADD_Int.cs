using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Add_Int : Action
{
    // The speed of the object
    public bool reset = false;    // The transform that the object is moving towards
    public SharedInt Number;
    public int direction;
   
    public override TaskStatus OnUpdate()
    {

        if (reset)
        {

            Number.Value = 0;
        }

        if (!reset)
            Number.Value++;
        return TaskStatus.Success;
    }
}