using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    private Player _target;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target != null)
        {
            Persecution();
        }
    }

    private void Persecution()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    public void DefineTarget(Player player)
    {
        _target = player;
    }

    public void FlyAway(float force, Player player)
    {
        _rigidbody.AddForce(player.transform.forward * force, ForceMode.Impulse);
    }
}
