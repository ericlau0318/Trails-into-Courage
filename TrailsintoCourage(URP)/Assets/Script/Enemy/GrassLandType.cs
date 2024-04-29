using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLandType : EnemyValue
{
    public Level1GameManager level1GameManager;
    
    public void ChasingPlayerGrassLand(GameObject enemy, Rigidbody rb, bool isAttack, bool inAttackArea, float movingSpeed)
    {
        // inital setting is 90 angle, but someone moodle 0 angle is different rotation state so need to try different change
        Rotation(playerCurrentPosition, enemy, rb, 90);
        if (spawner.grassLand && !isAttack && !inAttackArea)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
        else if (spawner.volcano || spawner.desert)
        {
            //Destroy(enemy);
        }
    }
    public void GrassLandEnemyDied()
    {
        if (enemyHealth <= 0)
        {
            stateController.GainExp(4);
            level1GameManager.AddKilledCount();
            spawner.monsterCount--;
            Destroy(gameObject);
        }
    }
}
