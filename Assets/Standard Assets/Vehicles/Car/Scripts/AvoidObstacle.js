// Avoid Obstacle Script
// ---------------------
 
var speed : float = 10.0;
var target : Vector3 = Vector3(0, 0, 0);
private var dir : Vector3;
private var dirFull : Vector3;
 
// ----
 
function FixedUpdate()
{
    // -- Obstacle Avoidance Tutorial --
 
    // the directional vector to the target
    dir = (target - transform.position).normalized;
    var hit : RaycastHit;
 
    // check for forward raycast
    if (Physics.Raycast(transform.position, transform.forward, hit, 1)) // 20 is raycast distance
    {
        if (hit.transform != this.transform)
        {
            Debug.DrawLine (transform.position, hit.point, Color.white);
 
            dir += hit.normal * 20; // 20 is force to repel by
        }
    }
 
    // more raycasts   
    var leftRay = transform.position + Vector3(-0.125, 0, 0);
    var rightRay = transform.position + Vector3(0.125, 0, 0);
 
    // check for leftRay raycast
    if (Physics.Raycast(leftRay, transform.forward, hit, 1)) // 20 is raycast distance
    {
        if (hit.transform != this.transform)
        {
            Debug.DrawLine (leftRay, hit.point, Color.red);
 
            dir += hit.normal * 20; // 20 is force to repel by
        }
    }
 
    // check for rightRay raycast
    if (Physics.Raycast(rightRay, transform.forward, hit, 1)) // 20 is raycast distance
    {
        if (hit.transform != this.transform)
        {
            Debug.DrawLine (rightRay, hit.point, Color.green);
 
            dir += hit.normal * 20; // 20 is force to repel by
        }
    }
 
    // --
 
    // Movement
 
    // rotation
    var rot = Quaternion.LookRotation (dir);
 
    //print ("rot : " + rot);
    transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime);
 
    //position
    transform.position += transform.forward * (2 * Time.deltaTime); // 20 is speed
 
    // -- end tutorial --
 
}