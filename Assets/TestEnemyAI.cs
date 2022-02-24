using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAI : MonoBehaviour
{
    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Test Death")]
    void DieTest()
    {
        enemyAI.Death();
    }
}
