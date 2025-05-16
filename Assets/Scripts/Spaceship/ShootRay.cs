using System;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootRay : MonoBehaviour
{
    [SerializeField] private float shootDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool isLog;
    private RaycastHit hit;
    private Ray ray;
    public static Action<Vector3,Vector3> OnShoot;

    public void ShootAtObjects(InputAction.CallbackContext context)
    {
        if(context.started)
        Logger("Attack");
        ray = new Ray(transform.position,transform.forward);

        if(Physics.Raycast(ray,out hit,shootDistance,layerMask))
        {
            Logger("Hit!");
            OnShoot?.Invoke(transform.position,hit.point);
            Logger(hit.collider.name);
            ColorChange(hit);
            Debug.DrawRay(transform.position,transform.forward,Color.red,5f);
        }
        else
        {
            Logger("Miss!");
            Vector3 end = transform.position + transform.forward * shootDistance;
            OnShoot?.Invoke(transform.position,end);
            Debug.DrawRay(transform.position,transform.forward,Color.yellow,5f);
        }
    }
    private void ColorChange(RaycastHit hit)
    {
        if(hit.collider.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.OnHit();
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
