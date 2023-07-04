using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    bool hasLanded;
    bool thrown;
    Vector3 startPos;
    public DiceSide[] TheDiceSides;
    public int DiceValue;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            SideValueCheck();
        }
        else if (rb.IsSleeping() && hasLanded && DiceValue == 0) 
        {
            RollAgain();
        }
    }

    void SideValueCheck()
    {
        DiceValue = 0;
        foreach(DiceSide side in TheDiceSides)
        {
            if (side.OnGround())
            {
                DiceValue = side.sideValue;
                //send result to game manager
                GameManager.Instance.RolledNumber(DiceValue);
            }
        }
    }

    void RollAgain()
    {
        ResetDice();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    public void RollDice()
    {
        
        ResetDice();

        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            ResetDice();
        }
    }

    void ResetDice()
    {
        transform.position = startPos;
        rb.isKinematic = false;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }
}
