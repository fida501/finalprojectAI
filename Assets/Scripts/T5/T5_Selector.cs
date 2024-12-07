using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_Selector : Node
    {
        public T5_Selector() : base()
        {
        }
        
        public T5_Selector(List<Node> children) : base(children)
        {
        }
        
        public override NodeState Evaluate()
        {
            bool anyChildSuccess = false;
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        anyChildSuccess = true;
                        break;
                }
            }

            state = anyChildSuccess ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }
    }
}
