using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // [SerializeField] private Tr spawnPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool isLog;
    private List<GameObject> Bullets = new List<GameObject>();
    private int poolAmount = 10;

    void OnEnable()
    {
        ShootRay.OnShoot += SpawnBullets;
    }
    void OnDisable()
    {
        ShootRay.OnShoot -= SpawnBullets;
    }
    void Start()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity);
            Bullets.Add(obj);
            obj.SetActive(false);
        }
    }

    private GameObject GetPooledBullet()
    {
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (!Bullets[i].activeInHierarchy)
            {
                return Bullets[i];
            }
        }
        return null;
    }

    public void SpawnBullets(Vector3 startPos, Vector3 targetPos)
    {
        GameObject bullet = GetPooledBullet();
        Logger("StartPos is : "+startPos);
        Logger("targetPos is : "+targetPos);
        if (bullet != null)
        {
            bullet.GetComponent<BulletTrail>().SetStartpos(startPos);
            bullet.GetComponent<BulletTrail>().SetTargetPos(targetPos);
            bullet.SetActive(true);
        }
    }

    private void Logger(string message)
    {
        if(isLog)
        {
            Debug.Log(message);
        }
    }
}

