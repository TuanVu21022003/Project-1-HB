using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject listBricksReceive;
    [SerializeField] Transform playerVisual;
    [SerializeField] private Transform parentPositon;
    [SerializeField] private float speed;
    private float brickPlaceUp = 0;
    private GameObject brickGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveController();
    }

    public void ChangPositionPlayerVisual(float distance)
    {
        playerVisual.localPosition += new Vector3(0, 0, distance);
    }
    [ContextMenu("AddBrick")]
    public void AddBrick()
    {
        AddBrickGround();
    }

    public void AddBrickGround()
    {
        GameObject brickPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_PREFAB}{KeyConstants.KEY_BRICK}");
        brickGround = Instantiate(brickPrefab, parentPositon);
        Vector3 brickSize = brickGround.GetComponent<Renderer>().bounds.size;
        brickGround.transform.localPosition = new Vector3(0, brickPlaceUp + brickSize.y, 0);
        brickPlaceUp += brickSize.y;
        ChangPositionPlayerVisual(brickSize.y);
    }

    public void MoveController()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Up");
            rb.velocity = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Up");
            rb.velocity = new Vector3(-1 * speed * Time.deltaTime , 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Up");
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.zero;
        }

    }
}
