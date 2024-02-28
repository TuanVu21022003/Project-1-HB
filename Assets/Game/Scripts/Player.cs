using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject listBricksReceive;
    [SerializeField] Transform playerVisual;
    [SerializeField] Animator anim;
    [SerializeField] private Transform parentPositon;
    [SerializeField] private StartRayCast startRayPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float distanceRay;
    private GameObject brickGround;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 posStop;
    private Vector3 direction;
    private Vector3 currentPosCast;
    private float brickPlaceUp = 0;
    public float swipeRange = 100f;
    private bool isPosStop = true;
    private int countBrickPlayer;
    private string currentAnim = "";
    private bool isControl = true;
    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        posStop = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveController();
    }

    public void SetIdle()
    {
        ChangAnim("Idle");
    }

    public void ChangPositionPlayerVisual(float distance)
    {
        playerVisual.localPosition += new Vector3(0, 0, distance);
    }
    [ContextMenu("AddBrick")]
    public void AddBrick()
    {
        ChangAnim("Jump");
        AddBrickGround();
        AddBrickPlayer();
        Invoke(nameof(SetIdle), 0.5f);
    }

    public void AddBrickPlayer()
    {
        countBrickPlayer++;
        Debug.Log(countBrickPlayer);
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

    public void RemoveBrick()
    {
        RemoveBrickGround();
        RemoveBrickPlayer();
    }

    public void RemoveBrickPlayer()
    {
        countBrickPlayer--;
        Debug.Log(countBrickPlayer);

    }

    public void RemoveBrickGround()
    {
        int countBrickReceive = listBricksReceive.transform.childCount;
        GameObject brickPlayer = listBricksReceive.transform.GetChild(countBrickReceive - 1).gameObject;
        Vector3 brickSize = brickPlayer.GetComponent<Renderer>().bounds.size;
        Destroy(brickPlayer);
        brickPlaceUp -= brickSize.y;
        ChangPositionPlayerVisual(-brickSize.y);
    }

    public void MoveControllerTest()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            
            rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            
            rb.velocity = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            rb.velocity = new Vector3(-1 * speed * Time.deltaTime , 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.zero;
        }

    }

    public void SetDirection()
    {
        if (isControl == false)
        {
            Debug.Log("Khong vuot duoc");
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPosStop = false;
            touchEndPos = Input.mousePosition;
            Vector3 swipeDirection = touchEndPos - touchStartPos;

            if (swipeDirection.magnitude > swipeRange)
            {
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    if (swipeDirection.x > 0)
                    {
                        // Vuốt sang phải
                        direction = Vector3.right;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                    }
                    else
                    {
                        // Vuốt sang trái
                        direction = Vector3.left;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                    }
                }
                else
                {
                    if (swipeDirection.y > 0)
                    {
                        // Vuốt lên trên
                        direction = Vector3.forward;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                    }
                    else
                    {
                        // Vuốt xuống dưới
                        direction= Vector3.back;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                    }
                }
            }
        }
    }

    public void MoveController()
    {
        //if(rb.velocity == Vector3.zero)
        //{
        //    ChangAnim("Idle");
        //}
        SetDirection();
        
        //Debug.Log(direction);
        if(isPosStop == false)
        {
            GameObject g = ObjectPooling.instance.getObject(startRayPrefab.gameObject);
            g.transform.position = currentPosCast;
            g.transform.rotation = Quaternion.identity;
            g.SetActive(true);
            StartRayCast startRay = g.GetComponent<StartRayCast>();
            isPosStop = startRay.IsFindWall(new Vector3(0,-1,0));
            startRay.OnInit();
            //Debug.Log(isPosStop);
            
            if(isPosStop == false)
            {
                posStop = currentPosCast - new Vector3(0, distanceRay, 0);
                currentPosCast += direction;
            }

        }
        if(countBrickPlayer > 0)
        {
            

            transform.position = Vector3.MoveTowards(transform.position, posStop, speed * Time.deltaTime);
         
            if(lastPosition != transform.position)
            {
                isControl = false;
                
            }
            else
            {
                isControl = true;
            }
            lastPosition = transform.position;
        }
        else 
        {
            rb.velocity = Vector3.zero;
            isPosStop = true;
            Debug.LogError("Thua");
        }

    }

    public void ChangAnim(string animName)
    {
        Debug.Log(currentAnim + " " + animName);
        if(currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chest")
        {

            if(other.gameObject.name == "Chests_Open")
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            else if(other.gameObject.name == "Chests_Close")
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            ChangAnim("Win");
        }
    }
}
