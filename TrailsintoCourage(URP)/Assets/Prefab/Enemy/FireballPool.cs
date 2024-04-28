using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    public static FireballPool instance;
    public GameObject fireballPrefab;
    public int fireBallID = 0;
    private Queue<GameObject> fireballList = new Queue<GameObject>();
    static readonly object _locker = new object();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public GameObject GetFireball()
    {
        GameObject result = null;
        if (fireballList.Count > 0)
        {
            result = fireballList.Dequeue();
        }
        else
        {
            result = Instantiate(fireballPrefab, transform);
            result.name = "B" + fireBallID;
            fireBallID++;
        }
        result.GetComponent<FireBall>().resultDefault();
        result.SetActive(false);
        return result;
    }
    public void ReleaseFireball(GameObject fireball)
    {
        fireball.SetActive(false);
        fireballList.Enqueue(fireball);
    }
}
