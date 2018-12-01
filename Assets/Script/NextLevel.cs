using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    public static int currentLevel = 1;
    public float speedIncrease = 0.1f;

    //Call to reference from other script
    public EnemyBullet enemyBullet;
    public BulletFollow bulletFollow;
    public BossBeam bossBeam;
    public EnemyBulletAOE enemyBulletAOE;
    // Start is called before the first frame update
    void Start()
    {
        //Getting the reference
        GameObject bulletRed = GameObject.Find("redCircle");
        enemyBullet = bulletRed.GetComponent<EnemyBullet>();

        GameObject beamFOLLOWED = GameObject.Find("beamFOLLOWED");
        bulletFollow = beamFOLLOWED.GetComponent<BulletFollow>();

        GameObject beam = GameObject.Find("beam");
        bossBeam = beam.GetComponent<BossBeam>();

        GameObject beamAOE = GameObject.Find("beamAOE");
        enemyBulletAOE = beamAOE.GetComponent<EnemyBulletAOE>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        //On Collision regenerate the level but harder
        if (other.gameObject.tag == "Player")
        {
            NextLevel.currentLevel++;
            enemyBullet.bulletSpeed =  enemyBullet.bulletSpeed + (enemyBullet.bulletSpeed * speedIncrease);
            bulletFollow.bulletSpeed =  bulletFollow.bulletSpeed + (bulletFollow.bulletSpeed * speedIncrease);
            bossBeam.bulletSpeed =  bossBeam.bulletSpeed + (bossBeam.bulletSpeed * speedIncrease);
            enemyBulletAOE.bulletSpeed =  enemyBulletAOE.bulletSpeed + (enemyBulletAOE.bulletSpeed * speedIncrease);
            Application.LoadLevel(1);
        }
    }
}
