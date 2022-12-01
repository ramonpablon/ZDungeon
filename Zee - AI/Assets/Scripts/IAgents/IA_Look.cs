using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_Look : Action {
    public InputManager_CPU CPU;
    // The transform that the object is moving towards
    public SharedTransform target;
    float timeLeft = 2f;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {

        timeLeft -= Time.deltaTime;


        if (target.Value.position.x < transform.position.x && !CPU.isStun && !CPU.inCombat)
        {
            CPU.angle = -90;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (target.Value.position.x > transform.position.x && !CPU.isStun && !CPU.inCombat)
        {
            CPU.angle = 90;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        return TaskStatus.Success;

    }
    
    
}
