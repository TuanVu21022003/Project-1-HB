using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _instance = null;
    public static ObjectPooling instance => _instance;

    private Dictionary<GameObject, List<GameObject>> _list = new Dictionary<GameObject, List<GameObject>>();
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        }
        if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getObject(GameObject defaultPrefab)
    {
        if (_list.ContainsKey(defaultPrefab))
        {
            foreach (GameObject o in _list[defaultPrefab])
            {
                if (o.activeSelf == false)
                {
                    return o;
                }
            }
            GameObject g = Instantiate(defaultPrefab, this.transform.position, this.transform.rotation);
            _list[defaultPrefab].Add(g);
            g.SetActive(false);
            return g;
        }
        List<GameObject> tmp = new List<GameObject>();
        GameObject g2 = Instantiate(defaultPrefab, this.transform.position, this.transform.rotation);
        g2.SetActive(false);
        tmp.Add(g2);
        _list.Add(defaultPrefab, tmp);
        return g2;
    }
}
