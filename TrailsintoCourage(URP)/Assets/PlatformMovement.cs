using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform startPoint; 
    public Transform endPoint; 
    public float speed = 1f; 
    private bool movingToEnd = true;

    private void Start()
    {
        StartCoroutine(MovePlatform());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

        private IEnumerator MovePlatform()
    {
        while (true)
        {
            Vector3 targetPosition = movingToEnd ? endPoint.position : startPoint.position;

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            movingToEnd = !movingToEnd;

            yield return null;
        }
    }
}
