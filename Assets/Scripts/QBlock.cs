using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlock : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;
    [SerializeField] private Transform spawnPoint;
    private Animator animator;
    private bool isAlive;

    public bool IsAlive { get => isAlive; }

    private void Awake()
    {
        isAlive = true;
        animator = GetComponentInChildren<Animator>();
    }

    public void Die()
    {
        if (!IsAlive)
            return;

        animator.SetBool("Die", true);
        isAlive = false;
        GameObject tmp = Instantiate(powerUp);
        tmp.transform.position = spawnPoint.position;
    }
}
