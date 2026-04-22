using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(MoveTo))]
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private List<Transform> _patrolPoints;
        public List<Transform> PatrolPoints
        {
            get => _patrolPoints;
            private set => _patrolPoints = value;
        }
        [SerializeField] private MoveTo _navMeshAgentMover;
        public float maxOffset = 0.12f;
        private int _currentPoint = 0;

        public Transform CurrentPoint
        {
            get => _patrolPoints[_currentPoint];
        }
        private void Start()
        {
            _navMeshAgentMover.GetComponent<MoveTo>();
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
