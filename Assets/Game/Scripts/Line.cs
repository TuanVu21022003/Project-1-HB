using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private GameObject parentPosition;
    private bool isActive = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isActive == false)
        {
            GameObject yellowPlanPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_PREFAB}{KeyConstants.KEY_YELLOWPLANE}");
            GameObject yellowPlane = Instantiate(yellowPlanPrefab, parentPosition.transform);
            yellowPlane.transform.position = this.transform.position + new Vector3(0, 0.03f, 0);
            other.gameObject.GetComponent<Player>().RemoveBrick();
            isActive = true;
        }
    }
}
