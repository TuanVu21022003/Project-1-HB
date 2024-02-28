using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit hit;
    private float maxDistance = 3f; // Khoảng cách tối đa tia có thể đi
    public bool IsFindWall(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, Color.red, 1000f);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            if(hitInfo.collider.gameObject.tag == "Wall" || hitInfo.collider.gameObject.tag == "Chest")
            {
                return true;
            }
        }
        return false;
    }

    public void OnInit()
    {
        Invoke(nameof(OnDespawn), 0.5f);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
