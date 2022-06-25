using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISimple : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public enum STATE
    {
        IDLE, PATROL, CHASE
    }
    public STATE currState = STATE.IDLE;

    public List<GameObject> patrolPoints = new List<GameObject>();
    int curPatrolIndex = -1; //The point of the patrol points where the enemy goes

    private float waitTimer = 0;

    private bool isInvincible = false;

    float visDist = 10.0f; //Distance of vision
    float visAngle = 90.0f; //Angle of the cone vision

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();

        if (patrolPoints.Count != 0) //Id there is patrol points
            ChangeState(STATE.PATROL);
    }

    void Update()
    {
        switch (currState)
        {
            case STATE.IDLE:
                if (CanSeePlayer())
                {
                    ChangeState(STATE.CHASE);
                }
                else if (Random.Range(0, 100) < 10)
                {
                    ChangeState(STATE.PATROL);
                }
                break;
            case STATE.PATROL:
                if (agent.remainingDistance < 1)
                {
                    if (curPatrolIndex >= patrolPoints.Count - 1)
                        curPatrolIndex = 0;
                    else
                        curPatrolIndex++;
                    agent.SetDestination(patrolPoints[curPatrolIndex].transform.position);
                }

                if (CanSeePlayer())
                {
                    ChangeState(STATE.CHASE);
                }
                break;
                 case STATE.CHASE:
                agent.SetDestination(player.position);
                if (agent.hasPath)
                {
                  
                    ChangeState(STATE.CHASE);
                    }
                break;
           
        }
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }


    public void ChangeState(STATE newState)
    {
        switch (currState)
        {
            case STATE.IDLE:
                anim.ResetTrigger("isIdle");
                break;
            case STATE.PATROL:
                anim.ResetTrigger("isPatrolling");
                break;
            case STATE.CHASE:
                anim.ResetTrigger("isChasing");
                break;
        }
        switch (newState)
        {
            case STATE.IDLE:
                anim.SetTrigger("isIdle");
                break;
            case STATE.PATROL:
                agent.speed = 2;
                agent.isStopped = false;

                float lastDist = Mathf.Infinity;
                for (int i = 0; i < patrolPoints.Count; i++)
                {
                    GameObject thisWP = patrolPoints[i];
                    float distance = Vector3.Distance(transform.position, thisWP.transform.position);
                    if (distance < lastDist)
                    {
                        curPatrolIndex = i - 1; //Because in the update it will be added one
                        lastDist = distance;
                    }
                }
                anim.SetTrigger("isPatrolling");
                break;
            case STATE.CHASE:
                agent.speed = 4;
                agent.isStopped = false;
                anim.SetTrigger("isChasing");
                break;
        }

        currState = newState;
    }

   

    private void LookPlayer(float speedRot)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        direction.y = 0;

        if(direction!= Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speedRot);
    }
}

