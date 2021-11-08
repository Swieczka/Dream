using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName =("Scriptable Objects/Enemy"))]
public class EnemyStats : ScriptableObject
{
    public string enemyName;

    public float enemyMovementSpeed;
    public float enemyAttackSpeed;
    public float enemyAttackRange;

    public float enemyAttack;
    public float enemyDefense;

    public float enemyLuck;
    public int enemyHealthPoints;


}
