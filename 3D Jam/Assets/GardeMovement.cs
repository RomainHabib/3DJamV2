using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GardeMovement : MonoBehaviour
{
    [Header("Game Design")]
    [Space(10)]
    [Range(0.0f, 20.0f)] public float speed;
    [Range(0.0f, 20.0f)] public float turnSpeed;

    public GameObject AWP, BWP;
    public Transform A, B, JAIL, INSIDE;
    public bool toA;
    private Vector3 pos;
    private bool normalMoving;

    public float rayMaxDistance;
    public float leftOff = -35f;
    public float leftOff1 = -17.5f;

    public float rightOff = 35f;
    public float rightOff1 = 17.5f;


    private float originalSpeed;
    private float originalTurnSpeed;
    public float timer;
    public bool hasChecked;
    private float jailOff;
    private float speedCoef;
    private float randomWait;
    private float randomAngleRotate;
    private float turnSpeedCoef;
    private float turnSpeedCoef2;
    private float minTurnSpeed;
    private float minSpeed;
    private float precision;
    private bool hasCheckedRandom;

    private float baseA, baseB, newA, newB;
    // Start is called before the first frame update
    void Start()
    {
        normalMoving = true;
        originalSpeed = speed;
        originalTurnSpeed = turnSpeed;
        baseA = A.transform.position.z;
        baseB = B.transform.position.z;
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        fromAtoB();
        Raycasts();
       
        checkJail();
    }

    void Randomize()
    {
        newA =  Random.Range(-5.0f, 5.0f);
        newB =  Random.Range(-5.0f, 5.0f);

        AWP.transform.Translate(0, 0, newA);
        BWP.transform.Translate(0, 0, newB);

        if (AWP.transform.position.z > JAIL.position.z) AWP.transform.Translate(0, 0, Random.Range(-15.0f, -5.0f));
        if (BWP.transform.position.z < JAIL.position.z) AWP.transform.Translate(0, 0, Random.Range(15.0f, 5.0f));


        jailOff = Random.Range(0.0f, 1.0f);
        speedCoef = Random.Range(1.0f, 1.5f);
        turnSpeedCoef = Random.Range(2.0f, 8.0f);
        turnSpeedCoef2 = Random.Range(0.5f, 1.0f);
        precision = Random.Range(1.0f, 6.0f);
        randomWait = Random.Range(4.0f, 8.0f);
        randomAngleRotate = Random.Range(20.0f, 360.0f);
        minTurnSpeed = Random.Range(0.6f, 1.0f);
        minSpeed = Random.Range(0.2f, 1.0f);
    }

    void checkJail()
    {
        if (Vector3.Distance(transform.position, JAIL.position) < 3  && hasChecked == false)
        {
            normalMoving = false;
        }
        else
        {
            normalMoving = true;
            timer = 0;
        }
    }


    void fromAtoB()
    {
        if (normalMoving)
        {
            if(speed < originalSpeed)
            {
                speed += Time.deltaTime*speedCoef;
            }
            if(turnSpeed < originalTurnSpeed)
            {
                turnSpeed += Time.deltaTime*turnSpeedCoef2;
            }

            if (toA)
            {
                transform.position = Vector3.MoveTowards(transform.position, A.position, speed * Time.deltaTime);

                Vector3 targetDirection = A.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

                if (Vector3.Distance(transform.position, A.position) < (Vector3.Distance(A.position, B.position) / 10))
                {
                    toA = false;
                    hasChecked = false;
                    Randomize();

                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, B.position, speed * Time.deltaTime);

                Vector3 targetDirection = B.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

                if (Vector3.Distance(transform.position, B.position) < (Vector3.Distance(A.position, B.position) / 10))
                {
                    toA = true;
                    hasChecked = false;
                    Randomize();

                }
            }
        }
        else
        {
            Vector3 vec = new Vector3();
            vec.x = 0;
            vec.y = 0;
            vec.z = jailOff;
            transform.position = Vector3.MoveTowards(transform.position, JAIL.position + vec, speed * Time.deltaTime);
            
            
            if(speed > minSpeed)
            {
                speed -= Time.deltaTime * speedCoef;
                hasCheckedRandom = false;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > randomWait)
                {
                    //speed = originalSpeed;
                   // turnSpeed = originalTurnSpeed;
                    hasChecked = true;
                    normalMoving = true;
                }
                else
                {
                    
                    if((transform.eulerAngles.y < randomAngleRotate - precision || transform.eulerAngles.y > randomAngleRotate + precision) && hasCheckedRandom == false)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, randomAngleRotate, 0), turnSpeed * Time.deltaTime);
                        if (turnSpeed > minTurnSpeed) turnSpeed -= Time.deltaTime * turnSpeedCoef;
                        else turnSpeed = minTurnSpeed;
                        //print("rotating to random angle: "  + randomAngleRotate);
                        //print(transform.eulerAngles.y);
                        //print(transform.rotation.y < randomAngleRotate - 10 || transform.rotation.y > randomAngleRotate + 10);
                        //print(transform.rotation.y + " < " + randomAngleRotate + " 10 " + "ou" + transform.rotation.y + " > " + randomAngleRotate + " 10");
                    }
                    else
                    {
                        hasCheckedRandom = true;
                        if(turnSpeed < originalTurnSpeed) turnSpeed += Time.deltaTime * turnSpeedCoef2;
                        Vector3 targetDirection = INSIDE.position - transform.position;
                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDirection);
                        //print("rotatin inside jail");
                    }


                        
                }
            }
            
           
        }

        


    }

    void Raycasts()
    {
        Vector3 defaultAngle = transform.forward;

        //--- Pour calculer les angles suivants un offset ---//
        Quaternion spreadAngleLeft = Quaternion.AngleAxis(leftOff, new Vector3(0, 1, 0));
        Quaternion spreadAngleLeft1 = Quaternion.AngleAxis(leftOff1, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight = Quaternion.AngleAxis(rightOff, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight1 = Quaternion.AngleAxis(rightOff1, new Vector3(0, 1, 0));

        Vector3 leftAngle = spreadAngleLeft * defaultAngle;
        Vector3 leftAngle1 = spreadAngleLeft1 * defaultAngle;

        Vector3 rightAngle = spreadAngleRight * defaultAngle;
        Vector3 rightAngle1 = spreadAngleRight1 * defaultAngle;


        Ray ray = new Ray(transform.position, transform.forward);
        Ray leftRay = new Ray(transform.position, leftAngle);
        Ray leftRay1 = new Ray(transform.position, leftAngle1);

        Ray rightRay = new Ray(transform.position, rightAngle);
        Ray rightRay1 = new Ray(transform.position, rightAngle1);



        RaycastHit hitInfo;

        //--- Conditions semblables à tous les raycast ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance) || Physics.Raycast(leftRay, out hitInfo, rayMaxDistance) || Physics.Raycast(rightRay, out hitInfo, rayMaxDistance))
        {
        }
        //--- Pour le raycast de face ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance))
        {
            //--- Si l'objet dans le Ray est un Prop ---//
            if (hitInfo.transform.CompareTag("Prop"))
            {
                //hitInfo.transform.GetComponent<Rigidbody>().AddForce(0,0,100);
                Debug.Log("Facing : " + hitInfo.transform.name);
            }

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayMaxDistance, Color.green);
        }


        //--- Pour le raycast de gauche ---//
        if (Physics.Raycast(leftRay, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(leftRay.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de gauche 1---//
        if (Physics.Raycast(leftRay1, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(leftRay1.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(leftRay1.origin, leftRay1.origin + leftRay1.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de droite ---//
        if (Physics.Raycast(rightRay, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(rightRay.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de droite 1---//
        if (Physics.Raycast(rightRay1, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(rightRay1.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rightRay1.origin, rightRay1.origin + rightRay1.direction * rayMaxDistance, Color.green);
        }
    }
}
