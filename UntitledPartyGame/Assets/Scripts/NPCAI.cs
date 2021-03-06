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
    public bool makeLeave = false;
    public GameObject[] waypoints;
    public float stunTime;
    public GameObject smokeObject;
    public Material chadMat;
    //public GameObject body;
    public GameObject head;
    public AudioClip knockoutSFX;

    private Vector3 nextDestination;
    private float distanceToPlayer;
    private int currentWaypoint = 0;
    private Animator anim;
    private NavMeshAgent nav;
    private INPCAttack attackMethod;
    private float currentStunTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        nav = this.GetComponent<NavMeshAgent>();
        attackMethod = this.GetComponent<INPCAttack>();
        currentStunTime = stunTime;

        if (isChad)
        {
            TurnChad();
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (makeLeave)
        {
            currentState = FSMStates.Leave;
        }

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
                UpdateStunState();
                break;
            case FSMStates.Leave:
                UpdateLeaveState();
                break;
        }
    }

    public void TurnChad()
    {
        smokeObject.SetActive(true);
        head.GetComponent<MeshRenderer>().material = chadMat;
        //body.GetComponent<MeshRenderer>().material = chadMat;

        isChad = true;
        this.gameObject.tag = "Chad";
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

        Debug.Log(distanceToPlayer.ToString());
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

        Debug.Log(distanceToPlayer.ToString());
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

    void UpdateStunState()
    {
        anim.SetInteger("animState", 3);
        currentStunTime -= Time.deltaTime;

        if (currentStunTime <= 0)
        {
            currentState = FSMStates.Idle;
            currentStunTime = stunTime;
        }
    }

    void UpdateLeaveState()
    {
        if (currentWaypoint >= waypoints.Length)
        {
            currentState = FSMStates.Idle;
        }
        else
        {
            anim.SetInteger("animState", 1);

            GameObject nextWaypoint = waypoints[currentWaypoint];
            FaceTarget(nextWaypoint.transform.position);
            nav.SetDestination(nextWaypoint.transform.position);

            float distanceToWaypoint = Vector3.Distance(this.transform.position, nextWaypoint.transform.position);
            Debug.Log(distanceToWaypoint.ToString());

            if (distanceToWaypoint <= attackDistance)
            {
                if (currentWaypoint < waypoints.Length)
                {
                    currentWaypoint++;

                    if (currentWaypoint >= waypoints.Length)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void MakeNPCLeave()
    {
        makeLeave = true;
        currentState = FSMStates.Leave;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox")
        {
            AudioSource.PlayClipAtPoint(knockoutSFX, transform.position);
            if (currentStunTime <= 0)
            {
                currentStunTime = stunTime;
            }
            currentState = FSMStates.Stun;
            TurnChad();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Throwable" || collision.gameObject.tag == "LiquorBottle")
        {
            AudioSource.PlayClipAtPoint(knockoutSFX, transform.position);
            if (currentStunTime <= 0)
            {
                currentStunTime = stunTime;
            }
            currentState = FSMStates.Stun;
            TurnChad();
        }
    }
}
