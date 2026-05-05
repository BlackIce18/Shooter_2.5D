using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(MoveTo))]
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private List<Transform> _patrolPoints;
        [SerializeField] private MoveTo _navMeshAgentMover;
        public List<Transform> PatrolPoints
        {
            get => _patrolPoints;
            private set => _patrolPoints = value;
        }
        public float maxOffset = 0.12f;
        private int _currentPoint = 0;

        public Transform CurrentPoint
        {
            get
            {
                if (_patrolPoints.Count > 0)  {return _patrolPoints[_currentPoint];}

                return null;
            }
        }

        public void MoveBetweenPoints()
        {
            if (Vector3.Distance(_navMeshAgentMover.transform.position, _navMeshAgentMover.Goal.position) <= maxOffset)
            {
                _currentPoint++;
                if (_currentPoint >= _patrolPoints.Count)
                {
                    _currentPoint = 0;
                }
                _navMeshAgentMover.Goal = _patrolPoints[_currentPoint].transform;
            }
        }
    }
}
