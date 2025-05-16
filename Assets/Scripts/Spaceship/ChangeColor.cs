using System.Collections;
using UnityEngine;

public class ChangeColor : MonoBehaviour,IDamagable
{
    private Renderer hitrenderer;
    [SerializeField] private bool isLog;
    void Start()
    {
        hitrenderer = GetComponent<Renderer>();
    }
    public void OnHit()
    {       
        StartCoroutine(GotHit());
    }  
    private IEnumerator GotHit()
    {
       if(hitrenderer != null)
       {
            hitrenderer.material.color = Color.red;
       }    

       yield return new WaitForSeconds(5f);

       hitrenderer.material.color = Color.grey;
    } 
    private void Logger(string message)
    {
        if(isLog)
        {
            Debug.Log(message);
        }
    }}
