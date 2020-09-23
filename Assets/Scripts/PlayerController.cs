using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float interPolation = 25f;
    [SerializeField] float laneDistance = 4f;
    [SerializeField] float increaseSpeedByTime = 0.1f;
    [SerializeField] float maxSpeed;
    //[SerializeField] Vector3 charSlideCenter = new Vector3(0,0.44f,0);
    Animator animator;
    bool slide = false;
    //[SerializeField] float smoothness = 200f;
    CharacterController characterController;
    private Vector3 dir;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gravity = -9.81f;
    int lane = 1;
    Collider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterController.Move(dir * Time.fixedDeltaTime);
    }
    private void Update()
    {
        //Physics.IgnoreCollision(playerCollider,hightObstColl.GetComponent<Collider>(),slide);       
        dir.z = speed;
        if (speed <= maxSpeed)
            speed += increaseSpeedByTime * Time.deltaTime;
        
        MoveBetweenLanes();
    }

    private void MoveBetweenLanes()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Jump"); 
            Jump();
        }
        else
        {
            dir.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lane++;
            if (lane == 3)
            {
                lane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lane--;
            if (lane == -1)
            {
                lane = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !slide)
        {
            //slide 
            StartCoroutine(Slide());
        }
        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lane == 0)
        {
            targetPos += Vector3.left * laneDistance;
        }
        else if (lane == 2)
        {
            targetPos += Vector3.right * laneDistance;
        }
        //transform.position = Vector3.Lerp(transform.position, targetPos, smoothness * Time.deltaTime);
        //characterController.center = characterController.center;
        if(transform.position == targetPos)
        {
            return;
        }
        Vector3 diff = targetPos - transform.position;
        Vector3 moveDir = diff.normalized * interPolation * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            characterController.Move(moveDir);
        }
        else
        {
            characterController.Move(diff);
        }

    }

    void Jump()
    {
        
        if (characterController.isGrounded)
        {
            dir.y = jumpForce;            
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
    IEnumerator Slide()
    {
        slide = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(1.3f);
        slide = false;
        animator.SetBool("isSliding", false);
    }
    public bool GetSlide()
    {
        return slide;
    }
}
