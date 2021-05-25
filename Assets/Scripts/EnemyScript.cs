using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject player;
    public Transform enemyRot;
    private Vector3 attackDir;
    private float enemySpeed = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerObject");
    }

    // Update is called once per frame
    void Update()
    {
        enemyRot.LookAt(player.transform.position);
        attackDir = new Vector3(
            ((player.transform.position.x - transform.position.x)),
            0,
            ((player.transform.position.z - transform.position.z))
        );
        attackDir = attackDir.normalized;
        transform.Translate(new Vector3(attackDir.x * Time.deltaTime*enemySpeed, 0, attackDir.z * Time.deltaTime*enemySpeed));
    }
}
