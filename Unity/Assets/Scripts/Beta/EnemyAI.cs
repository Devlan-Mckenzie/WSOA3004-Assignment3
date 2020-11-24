using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header ("Player is Target")]
    //the point the enemy wants to reach
    public Transform target;
    
    private float speed=100;
    public float speedmultiplier=1;
    public float nextWayPointDistance = 3f;

    [Header ("Enemy")]
    //for the purpose of fliping the sprite depending on the direction of movement
    public Transform enemyimage;
    public float scalex;
    public float scaley;

    Path path;
    int currentwaypoint;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody;
    
    public Vector3 walkingdirection;
    public Vector2 NextWayPoint;

    public Vector2 force;
   
    // Start is called before the first frame update
    void Start()
    {
        seeker = this.GetComponent<Seeker>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        //find a new path every 00.25s
        InvokeRepeating("UpdatePath", 0f, 0.25f);
        
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            //create a path starting from the position of the enemy and ending at the target(player)
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            //if there is a path that can be made , make it and set the current wayypoint to 0
            path = p;
            currentwaypoint = 0;
        }
    }

    
    //make the srpite look towards their next point on the path
    public void aim()
    {

        walkingdirection = ((Vector2)path.vectorPath[currentwaypoint] - rigidbody.position).normalized; 
        transform.up = walkingdirection;
    }

    

    void FixedUpdate()
    {
        

        if(path == null)
        {
            return;
        }

        if(currentwaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }

        else
        {
            reachedEndofPath = false;
        }
        //find the direction that the enemy must move
        Vector2 direction = ((Vector2) path.vectorPath[currentwaypoint] - rigidbody.position).normalized;
        //create a force to move the enemy in the desired direction
        //force = direction * speed * Time.deltaTime;

        if (FindObjectOfType<EnemyController>().isChasing)
        {
            //move the enemy
            rigidbody.velocity = direction * speed * Time.deltaTime;
        }
        
        
        
        //finding the distance between the enemy annd its next waypoint
        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentwaypoint]);

        if(distance < nextWayPointDistance)
        {
            //if the distance becomes smaller than the next waypoint distance create another waypoint
            currentwaypoint++;
        }

        if(rigidbody.velocity.x >= 0.01f)
        {
            enemyimage.localScale = new Vector3(-scalex, scaley, 1);

        }

        else if (rigidbody.velocity.x <= 0.01f)
        {
            enemyimage.localScale = new Vector3(scalex, scaley, 1);

        }


        //aim();

        NextWayPoint = (Vector2)path.vectorPath[currentwaypoint];

    }
}
