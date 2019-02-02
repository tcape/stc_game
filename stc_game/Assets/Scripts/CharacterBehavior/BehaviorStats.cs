using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class BehaviorStats : ScriptableObject
{
    public float headOffset = 1.7f;
    public float headGizmoRadius = 0.5f;
    public float stopFollowDistance = 15f;
    public float patrolStopDistance = 0.2f;
    public float rotationSpeed = 15f;
    public float aggroDistance = 15f;
    public float idleWaitTime = 5f;
    public bool waiting = false;
   
}
