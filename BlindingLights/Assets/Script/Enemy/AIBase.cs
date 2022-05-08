using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EAIState
{
    Patrol,
    Attack
}

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : MonoBehaviour
{
    EAIState currentState = EAIState.Patrol;
    NavMeshAgent navAgent = null;

    GameObject Target = null;

    float UpdateRate = 0.05f;

    [SerializeField] float attackRange = 1.5f;

    [SerializeField] int impactDamage = 1;

    HealthComponent health;


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthComponent>();

        StartExecution();
    }

    private void Start()
    {
        GetPlayer();
    }

    void GetPlayer()
    {
        GameObject _Player = BasicBehaviour.playerController.gameObject; // need player to spawn before this function get called
        if (_Player)
        {
            Target = _Player;
            return;
        }

        Debug.LogError(name + " PlayerController reference is INVALID");
    }

    void StartExecution()
    {
        InvokeRepeating("ExecuteState", 0, UpdateRate);
    }

    void ExecuteState()
    {
        switch (currentState) // check if the currentState is one of the case
        {
            case EAIState.Patrol: //if Patrol, then call the PatrolState
                PatrolState();
                break;

            case EAIState.Attack: // if Attack, then call the AttackState
                AttackState();
                break;
        }
    }

    void PatrolState()
    {
        if (Target == null) return; // only one line so just do like this, line below will be a false statement

        navAgent.SetDestination(Target.transform.position); // using AI system to move the target

        //Deal Damage
        float _distance = 0;

        _distance = Vector3.Distance(gameObject.transform.position, Target.transform.position); // returns the distance from point A to B

        if (_distance < attackRange)         // if the distance is lesser than the attackRange, initiate AttackState. P.S not using if check collider tag anymore
        {
            currentState = EAIState.Attack;
        }
    }

    void AttackState()
    {
        GameplayStatics.DealDamage(Target, impactDamage);
        GameplayStatics.DealDamage(gameObject, 100);
        Destroy(gameObject);
    }
}

