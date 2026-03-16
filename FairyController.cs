using UnityEngine;

public class FairyController : MonoBehaviour
{

    public GameObject player; 
    public float smoothTime = 0.3f;
    public float orbitRadius = 0.3f;
    public float orbitSpeed = 50f;
    public float verticalSpeed = 1.4f;
    public float oscillationSpeed = 4.2f;
    public float waveSpeed = 1.4f;
    
    GameObject fairyChildTransform;
    Vector3 lastPlayerPosition;
    Vector3 fairyVelocity = Vector3.zero;
    Vector3 targetPosition;
    float distance;
    bool isMoving = false;
    int direction = 1;
    float timeReference = 0f;
    

    
    void Start()
    {
        lastPlayerPosition = player.transform.position;
        fairyChildTransform = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // the target position is close to the character's shoulder
        targetPosition = player.transform.position + new Vector3(orbitRadius, 1f, 0f);
        //distance = Vector3.Distance(player.transform.position + new Vector3(0f, 1.5f, 0f), transform.position);
        if(player.transform.position != lastPlayerPosition )
            isMoving = true;
        else
        {
            if(Vector3.Distance(transform.position,targetPosition) < 0.01f) //check if the fairy reached the character's shoulder
                isMoving = false;
        }
        lastPlayerPosition = player.transform.position;

        if(isMoving) 
        {
            Follow();
        }
        else
        {
            Orbit();
        }
    }

    void Follow()
    {
        float w; //wave motion

        timeReference = 0f; //reset time reference for orbiting
        // Follow the player
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref fairyVelocity, smoothTime);

        // Make the movement wavy
        w = Mathf.Sin(Time.time * waveSpeed) * 0.4f;
        fairyChildTransform.transform.localPosition = new Vector3(w, 0f, 0f);
        
    }

    void Orbit()
    {
        float y1; //vertical motion
        float y2; //oscillations

        //Rotate aroud the player
        transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed*Time.deltaTime); 

        //Add vertical movement and oscillations
        y1 = Mathf.Sin(timeReference * verticalSpeed) * 0.4f;
        y2 = Mathf.Sin(timeReference * oscillationSpeed) * 0.1f;
        
        transform.position = new Vector3(transform.position.x, targetPosition.y + y1 + y2 , transform.position.z); 
        timeReference += Time.deltaTime;
    } 
}
