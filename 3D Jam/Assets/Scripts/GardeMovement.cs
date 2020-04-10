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


    [Header("Autre")]
    public bool isChecking;

    public float chancesOfSkipping;
    public float tpChances;

    public GameObject AWP, BWP, JAILWP;
    public Transform A, B, JAIL, INSIDE;
    public bool toA;
    private Vector3 pos;
    private bool normalMoving;

    public float rayMaxDistance;
    public float leftOff = -35f;
    public float leftOff1 = -17.5f;
    public float leftOff2 = -8.75f;
    public float leftOff3 = -26.25f;


    public float rightOff = 35f;
    public float rightOff1 = 17.5f;
    public float rightOff2 = 8.75f;
    public float rightOff3 = 26.25f;


    private float originalSpeed;
    private float originalTurnSpeed;
    public float timer;
    public bool hasChecked;
    

    [Header("Dont modify here")]
    public float jailOff;
    public float speedCoef;
    public float randomWait;
    public float randomAngleRotate;
    public float turnSpeedCoef;
    public float turnSpeedCoef2;
    public float minTurnSpeed;
    public float minSpeed;
    public float precision;
    public bool hasCheckedRandom;
    
    public float skipRng;
    public float tpRng;

    private float newA, newB;

    private Animator getAnim;

    // Start is called before the first frame update
    void Start()
    {
        normalMoving = true;
        originalSpeed = speed;
        originalTurnSpeed = turnSpeed;
        Randomize();
        getAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fromAtoB();
        Raycasts();
        checkJail();
        ChangeAnim();
    }

    void Randomize()
    {
        newA =  Random.Range(-5.0f, 4.0f);
        newB =  Random.Range(-4.0f, 5.0f);

        AWP.transform.Translate(0, 0, newA);
        BWP.transform.Translate(0, 0, newB);

        newA = Random.Range(-1.5f, 1.5f);
        newB = Random.Range(-1.5f, 1.5f);

        AWP.transform.Translate(newA, 0, 0);
        BWP.transform.Translate(newB, 0, 0);
        JAILWP.transform.Translate(Random.Range(-1.5f, 1.5f), 0,0);


        if (AWP.transform.position.z > JAIL.position.z - 8) AWP.transform.Translate(0, 0, Random.Range(-15.0f, -6.0f));
        if (BWP.transform.position.z < JAIL.position.z + 8) AWP.transform.Translate(0, 0, Random.Range(6.0f, 15.0f));

        if (AWP.transform.position.x > 10) AWP.transform.Translate(Random.Range(-2.5f, -1.5f), 0,0 );
        if (BWP.transform.position.x > 10) BWP.transform.Translate(Random.Range(-2.5f, -1.5f), 0, 0);
        if (AWP.transform.position.x < 7) AWP.transform.Translate(Random.Range(1.5f, 3.5f), 0, 0);
        if (BWP.transform.position.x < 7) BWP.transform.Translate(Random.Range(1.5f, 3.5f), 0, 0);

        if (JAILWP.transform.position.x < 7) JAILWP.transform.Translate(Random.Range(1.5f, 2.5f), 0, 0);
        if (JAILWP.transform.position.x > 10) JAILWP.transform.Translate(Random.Range(-2.5f, -1.5f), 0, 0);


        jailOff = Random.Range(0.0f, 1.0f);
        speedCoef = Random.Range(1.0f, 1.5f);
        turnSpeedCoef = Random.Range(2.0f, 8.0f);
        turnSpeedCoef2 = Random.Range(0.5f, 1.0f);
        precision = Random.Range(1.0f, 6.0f);
        randomWait = Random.Range(4.0f, 7.0f);
        randomAngleRotate = Random.Range(20.0f, 360.0f);
        minTurnSpeed = Random.Range(0.6f, 1.0f);
        minSpeed = Random.Range(0.2f, 1.0f);

        skipRng = Random.Range(0.0f, 100.0f);
        tpRng = Random.Range(0.0f, 100.0f);

    }

    void checkJail()
    {
        if (Vector3.Distance(transform.position, JAIL.position) < 3  && hasChecked == false && skipRng > chancesOfSkipping)
        {
            normalMoving = false;
            isChecking = true;
        }
        else
        {
            normalMoving = true;
            isChecking = false;
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

                if (Vector3.Distance(transform.position, A.position) < (Vector3.Distance(A.position, B.position) / 20))
                {
                    toA = false;
                    hasChecked = false;
                    Randomize();
                    if (tpRng < tpChances)
                    {
                        transform.position = B.position;
                        tpRng = 200;
                    }
                    

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
                    if (tpRng < tpChances)
                    {
                        transform.position = A.position;
                        tpRng = 200;

                    }
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
        Quaternion spreadAngleLeft2 = Quaternion.AngleAxis(leftOff2, new Vector3(0, 1, 0));
        Quaternion spreadAngleLeft3 = Quaternion.AngleAxis(leftOff3, new Vector3(0, 1, 0));

        Quaternion spreadAngleRight = Quaternion.AngleAxis(rightOff, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight1 = Quaternion.AngleAxis(rightOff1, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight2 = Quaternion.AngleAxis(rightOff2, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight3 = Quaternion.AngleAxis(rightOff3, new Vector3(0, 1, 0));


        Vector3 leftAngle = spreadAngleLeft * defaultAngle;
        Vector3 leftAngle1 = spreadAngleLeft1 * defaultAngle;
        Vector3 leftAngle2 = spreadAngleLeft2 * defaultAngle;
        Vector3 leftAngle3 = spreadAngleLeft3 * defaultAngle;


        Vector3 rightAngle = spreadAngleRight * defaultAngle;
        Vector3 rightAngle1 = spreadAngleRight1 * defaultAngle;
        Vector3 rightAngle2 = spreadAngleRight2 * defaultAngle;
        Vector3 rightAngle3= spreadAngleRight3 * defaultAngle;


        Vector3 posRaycast = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);

        Ray ray = new Ray(posRaycast, transform.forward);
        Ray leftRay = new Ray(posRaycast, leftAngle);
        Ray leftRay1 = new Ray(posRaycast, leftAngle1);
        Ray leftRay2 = new Ray(posRaycast, leftAngle2);
        Ray leftRay3 = new Ray(posRaycast, leftAngle3);


        Ray rightRay = new Ray(posRaycast, rightAngle);
        Ray rightRay1 = new Ray(posRaycast, rightAngle1);
        Ray rightRay2 = new Ray(posRaycast, rightAngle2);
        Ray rightRay3 = new Ray(posRaycast, rightAngle3);


        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        RaycastHit hitInfo;

        //--- Conditions semblables à tous les raycast ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(leftRay, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(rightRay, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(leftRay1, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(rightRay1, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(rightRay2, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(rightRay3, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(leftRay2, out hitInfo, rayMaxDistance, layerMask) || Physics.Raycast(leftRay3, out hitInfo, rayMaxDistance, layerMask))
        {
            if (isChecking)
            {
                // INTEGRER LA BOOL SUSPICIOUS

             
              // CanvasManager.Instance.setLooseMenu();
  

            }
        }
        //--- Pour le raycast de face ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance, layerMask))
        {
            //--- Si l'objet dans le Ray est un Prop ---//
            if (hitInfo.transform.CompareTag("Player"))
            {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayMaxDistance, Color.green);
        }


        //--- Pour le raycast de gauche ---//
        if (Physics.Raycast(leftRay, out hitInfo, rayMaxDistance, layerMask))
        {
            //--- Si l'objet dans le Ray est un Prop ---//
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(leftRay.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(leftRay.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de gauche 1---//
        if (Physics.Raycast(leftRay1, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(leftRay1.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(leftRay1.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(leftRay1.origin, leftRay1.origin + leftRay1.direction * rayMaxDistance, Color.green);
        }

        if (Physics.Raycast(leftRay2, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(leftRay2.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(leftRay2.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(leftRay2.origin, leftRay2.origin + leftRay2.direction * rayMaxDistance, Color.green);
        }

        if (Physics.Raycast(leftRay3, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(leftRay3.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(leftRay3.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(leftRay3.origin, leftRay3.origin + leftRay3.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de droite ---//
        if (Physics.Raycast(rightRay, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(rightRay.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(rightRay.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de droite 1---//
        if (Physics.Raycast(rightRay1, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(rightRay1.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(rightRay1.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(rightRay1.origin, rightRay1.origin + rightRay1.direction * rayMaxDistance, Color.green);
        }

        if (Physics.Raycast(rightRay2, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(rightRay2.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(rightRay2.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(rightRay2.origin, rightRay2.origin + rightRay2.direction * rayMaxDistance, Color.green);
        }

        if (Physics.Raycast(rightRay3, out hitInfo, rayMaxDistance, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                Debug.DrawLine(rightRay3.origin, hitInfo.point, Color.red);

            }
            else
            {
                Debug.DrawLine(rightRay3.origin, hitInfo.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(rightRay3.origin, rightRay3.origin + rightRay3.direction * rayMaxDistance, Color.green);
        }
    }

    void ChangeAnim()
    {
        if (isChecking)
        {
            getAnim.SetBool("isChecking", true);
        }
        else
        {
            getAnim.SetBool("isChecking", false);
        }
    }
}
