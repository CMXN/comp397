using UnityEngine;

namespace Platformer397
{
    public class EnemyNavigation : MonoBehaviour
    {

        private UnityEngine.AI.NavMeshAgent agent;
        private Transform player;
        private void Awake()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            player = GameObject.FindWithTag("Player").transform;
        }
        void Update()
        {
                agent.destination = player.position;
        }
    }
}
