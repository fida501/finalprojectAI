using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_Sequence : Node
    {
        public T5_Sequence() : base()
        {
        }
        
        public T5_Sequence(List<Node> children) : base(children)
        {
        }
        
        public override NodeState Evaluate()
        {
            bool anyChildRunning = false;
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        break;
                }
            }

            state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}