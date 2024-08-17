using Gmtk.Robot.AI;
using UnityEngine;

namespace Gmtk.Map
{
    public class BuildArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject _aiPrefab;

        private AIController _constructing;

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void Spawn()
        {
            var go = Instantiate(_aiPrefab, transform.position, Quaternion.identity);
            _constructing = go.GetComponent<AIController>();
        }
    }
}
