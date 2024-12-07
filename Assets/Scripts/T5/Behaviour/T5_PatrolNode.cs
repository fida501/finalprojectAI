using System.Collections.Generic;
using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_PatrolNode : Node
    {
        private Transform _transform;
        private List<Transform> _patrolPoints;
        private int _currentPoint = 0;
        private float _speed;

        public T5_PatrolNode(Transform transform, List<Transform> patrolPoints, float speed)
        {
            _transform = transform;
            _patrolPoints = patrolPoints;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            Transform targetPoint = _patrolPoints[_currentPoint];
            if (Vector2.Distance(_transform.position, targetPoint.position) < 0.1f)
            {
                _currentPoint = (_currentPoint + 1) % _patrolPoints.Count;
                state = NodeState.SUCCESS;
                return state;
            }

            _transform.position = Vector2.MoveTowards(_transform.position, targetPoint.position, _speed * Time.deltaTime);
            state = NodeState.RUNNING;
            return state;
        }
    }
}