using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQBlock : MonoBehaviour
{
    private QBlock qBlock;

    private void Awake()
    {
        qBlock = GetComponent<QBlock>();
    }

    void Update()
    {

    }

    [ContextMenu("Test Qblock")]
    private void TestQblockDeath()
    {
        qBlock.Die();
    }
}
