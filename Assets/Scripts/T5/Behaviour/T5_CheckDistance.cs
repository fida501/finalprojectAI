using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_CheckDistance : Node
    {
        private Transform _transform;
        private Transform _target;

        public T5_CheckDistance(Transform transform, Transform target)
        {
            _transform = transform;
            _target = target;
        }

        public override NodeState Evaluate()
        {
            if (_transform == null || _target == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector2.Distance(_transform.position, _target.position) < 0.1f)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}