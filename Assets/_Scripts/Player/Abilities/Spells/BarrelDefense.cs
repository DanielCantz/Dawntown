using UnityEngine;

public class BarrelDefense : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    private bool isJumping;
    private Vector3 jumpPosition;
      
    private GameObject player;
    [SerializeField] private float jumpForce;

    private int speed;
    private bool isGrounded=true;


    private void FixedUpdate()
    {      
        var i = 0f;
        if (isJumping)
        {           
            if (isGrounded)
            {                  
                while (i < jumpDuration)
                {
                    //jumpForce = 3;
                    speed = 1;
                    //move Player to jumpPosition  
                    player.GetComponent<Rigidbody>().AddForce(new Vector3(0f, jumpForce, 0f));
                    player.GetComponent<Rigidbody>().MovePosition(player.transform.position + jumpPosition * Time.fixedDeltaTime * speed);
                    i += Time.deltaTime;
                }
                isJumping = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/PC/Jump/JumpOff");
                
            }
        }
    }  
         
    public void Jumping(Vector3 endposition, float cooldown, float lifetime)
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreLayerCollision(8, 10);
        jumpDuration = lifetime;
        isJumping = true;
        jumpPosition = endposition;
        //FMODUnity.RuntimeManager.PlayOneShot("event:/PC/Jump/JumpOff");
        

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //print("OnCollisionEnter");
        }
    }
    void OnCollisionExit(Collision other)

    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            //print("OnCollisionExit");
        }
    }


}


