using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T5_BehaviourTree
{
    public abstract class T5_Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        protected void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }
        
        protected abstract Node SetupTree();
    }
}