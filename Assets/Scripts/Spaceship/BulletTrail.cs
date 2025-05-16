using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    private float progress;
    private bool canStart;

    [SerializeField] private int speed = 5;

    void Update()
    {
        if (canStart)
        {
            progress += speed * Time.deltaTime ;
        }

        transform.position = Vector3.Lerp(startPos, endPos, progress);  

        if(transform.position == endPos)
        {
            ResetBulletInfo();
        }
    }
    public void SetTargetPos(Vector3 EndPos)
    {
        endPos = EndPos;
        gameObject.SetActive(true);
        canStart = true;
    }
    public void SetStartpos(Vector3 StartPos)
    {
        startPos = StartPos;
        transform.position = StartPos;
    }
    private void ResetBulletInfo()
    {
        gameObject.SetActive(false);
        transform.position = startPos;
        progress = 0;
        canStart = false;
    }
}
