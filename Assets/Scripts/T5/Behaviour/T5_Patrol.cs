using System.Collections.Generic;
using UnityEngine;

namespace T5_BehaviourTree
{
    public class T5_Patrol : T5_Tree
    {
        public List<Transform> patrolPoints;
        public float speed = 2f;
        public float waitTime = 2f;
        private Transform _transform;
        private int _currentPoint = 0;
        private T5_MoveTo _moveToNode;
        private bool _isWaiting = false;
        private float _waitTimer = 0f;
        private bool _isChasing = false;
        private Transform _playerTransform;
        private BoxCollider2D _detectorCollider;

        private void Awake()
        {
            _transform = transform;
            _detectorCollider = GetComponentInChildren<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("T5_Player"))
            {
                _playerTransform = other.transform;
                _isChasing = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("T5_Player"))
            {
                _isChasing = false;
                _moveToNode.SetTarget(patrolPoints[_currentPoint]); // Reset target to current patrol point
            }
        }

        protected override Node SetupTree()
        {
            _moveToNode = new T5_MoveTo(_transform, patrolPoints[_currentPoint], speed);
            return new T5_Sequence(new List<Node>
            {
                _moveToNode,
                new T5_CheckDistance(_transform, patrolPoints[_currentPoint])
            });
        }

        private void Update()
        {
            if (_isChasing)
            {
                _moveToNode.SetTarget(_playerTransform);
                _moveToNode.Evaluate();
                return;
            }

            if (_isWaiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= waitTime)
                {
                    _isWaiting = false;
                    _waitTimer = 0f;
                    _currentPoint = (_currentPoint + 1) % patrolPoints.Count;
                    _moveToNode.SetTarget(patrolPoints[_currentPoint]);
                }
                return;
            }

            if (_moveToNode.Evaluate() == NodeState.SUCCESS)
            {
                _isWaiting = true;
            }
        }
    }
}