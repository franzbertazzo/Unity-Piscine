using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject pivot;
    [SerializeField] float stopingVelocity;
    [SerializeField] float rotationSpeed;
    [SerializeField] float forceRating;
    [SerializeField] float fillFactor;
    [SerializeField] GameObject powerBar;
    
    
    public Transform target;
    bool canCharge;
    bool isCharging;
    bool isShooting;
    
    Vector3 direction;
    float rotation;
    float power;
    Image bar;

    int numberOfPresses;

    void Start()
    {
        power = 0;
        isCharging = false;
        isShooting = false;
        canCharge = false;
        numberOfPresses = 0;
        direction = new Vector3(0,0,0);
        rotation = 0;
        target = ball.transform;
        bar = powerBar.GetComponent<Image>();
    }
    
    void Update()
    {
        CheckInput();
        CheckRotationMovement();
        CheckBallForce();
        CheckBallStop();
        bar.fillAmount = power / forceRating;
    }

    void CheckRotationMovement()
    {
        direction = pivot.transform.forward;
        pivot.transform.Rotate(0, rotation, 0);
    }

    void CheckInput()
    {
        rotation = Input.GetAxis("Horizontal")*rotationSpeed*Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            numberOfPresses++;
        }
        if (numberOfPresses == 1)
        {
            isCharging = true;
        }
        if (numberOfPresses == 2)
        {
            isCharging = false;
        }
        if (numberOfPresses == 3)
        {
            Shoot();
        }
    }

    void CheckBallForce()
    {
        if (canCharge)
        {
            if (isCharging)
            {
                if (power < forceRating)
                {
                    power = power + Time.deltaTime * fillFactor;
                }
                else
                {
                    isCharging = false; 
                }
            }
            if (isCharging == false)
            {
                if (power > 0)
                {
                    power = power - Time.deltaTime * fillFactor;
                }
                else
                {
                    isCharging = false; 
                }
            }

            
        }

        

        Debug.Log(power);
    }

    void CheckBallStop()
    {
        if (ball.GetComponent<Rigidbody>().velocity.magnitude < stopingVelocity)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.rotation = Quaternion.identity;
        }
    }

    void Shoot()
    {
        if (isShooting == false)
        {
            ball.GetComponent<Rigidbody>().AddForce(direction * forceRating);
            isShooting = true;
        }
    }
}
