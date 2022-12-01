using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IA_Defend : Action {
    public InputManager_CPU CPU;
    // The transform that the object is moving towards
    public bool Ativardefesa;
    public SharedTransform target;
    public override void OnAwake()
    {
        CPU = GetComponent<InputManager_CPU>();
    }
    public override TaskStatus OnUpdate()
    {
        
        
            CPU.Defend = Ativardefesa;
        
        return TaskStatus.Success;

    }
}
