﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Follow,
        Attack,
        Stun,
        Leave
    }

    public FSMStates currentState;
    public GameObject player;
    public float followDistance = 10f;
    public float attackDistance = 1f;
    public Transform eyes;
    public float fieldOfView = 45f;
    public bool isChad = false;

    private Vector3 nextDestination;
    private float distanceToPlayer;
    private GameObject[] waypoints;
    private Animator anim;
    private NavMeshAgent nav;
    private INPCAttack attackMethod;

    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        anim = this.GetComponent<Animator>();
        nav = this.GetComponent<NavMeshAgent>();
        attackMethod = this.GetComponent<INPCAttack>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Follow:
                UpdateFollowState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Stun:

                break;
            case FSMStates.Leave:
                break;
        }
    }

    public void TurnChad()
    {
        // Create some Chad effect
        isChad = true;
    }

    void UpdateIdleState()
    {
        anim.SetInteger("animState", 0);

        if (isChad && (distanceToPlayer <= followDistance || IsPlayerInFOV()))
        {
            currentState = FSMStates.Follow;
        }
    }

    void UpdateFollowState()
    {
        anim.SetInteger("animState", 1);

        nav.stoppingDistance = attackDistance;
        FaceTarget(player.transform.position);
        nav.SetDestination(player.transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > followDistance)
        {
            nav.ResetPath();
            currentState = FSMStates.Idle;
        }
    }

    void UpdateAttackState()
    {
        nav.stoppingDistance = attackDistance;

        if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Follow;
        }
        else if (distanceToPlayer > followDistance)
        {
            nav.ResetPath();
            currentState = FSMStates.Idle;
        }

        FaceTarget(player.transform.position);
        nav.SetDestination(player.transform.position);

        anim.SetInteger("animState", 2);
        attackMethod.Attack();
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    // Credit to IsPlayerInFOV code to Prof. Caglar Yildirim
    bool IsPlayerInFOV()
    {
        Vector3 directionToPlayer = player.transform.position - eyes.position;
        RaycastHit hit;

        if (Vector3.Angle(directionToPlayer, eyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(eyes.position, directionToPlayer, out hit, followDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
