using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfter : MonoBehaviour
{
    public float deleteAfterSeconds;
    void Start()
    {
        StartCoroutine(deleteAfter());
    }

    IEnumerator deleteAfter()
    {
        yield return new WaitForSeconds(deleteAfterSeconds);
        Destroy(gameObject);
    }
}
