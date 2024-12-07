using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_MoveTo : Node
    {
        private Transform _transform;
        private Transform _target;
        private float _speed;

        public T5_MoveTo(Transform transform, Transform target, float speed)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
        }

        public void SetTarget(Transform target)
        {
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

            _transform.position = Vector2.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
            state = NodeState.RUNNING;
            return state;
        }
    }
}