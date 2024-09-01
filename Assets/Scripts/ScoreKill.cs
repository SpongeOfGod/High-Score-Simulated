using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKill : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MessageAppear());
    }

    IEnumerator MessageAppear() 
    {
        yield return new WaitForSeconds(1.01f);
        Destroy(gameObject);
    }
}
