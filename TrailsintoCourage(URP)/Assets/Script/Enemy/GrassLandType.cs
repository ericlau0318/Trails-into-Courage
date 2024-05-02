using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLandType : EnemyValue
{
    public Level1GameManager level1GameManager;
    
    public void ChasingPlayerGrassLand(GameObject enemy, Rigidbody rb, bool isAttack, bool inAttackArea, float movingSpeed, float rotateAngle)
    {
        // inital setting is 90 angle, but someone moodle 0 angle is different rotation state so need to try different change
        Rotation(playerCurrentPosition, enemy, rb, rotateAngle);
        if (spawner.grassLand && !isAttack && !inAttackArea)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
        else if (spawner.volcano || spawner.desert)
        {
            //Destroy(enemy);
        }
    }
    public void GrassLandEnemyDied(int exp)
    {
        if (enemyHealth <= 0)
        {            
            isdead = true;
            Destroy(gameObject, 0.5f);
            this.gameObject.SetActive(false);
        }
        if (isdead)
        {
            stateController.GainExp(exp);
            level1GameManager.AddKilledCount();
            spawner.monsterCount--;
            isdead = false;
        }
    }
}
