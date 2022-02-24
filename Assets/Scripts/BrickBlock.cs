using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    /// <summary>
    /// Breaks the block
    /// </summary>
    public void BreakBlock()
    {
        gameObject.SetActive(false);
    }
}
