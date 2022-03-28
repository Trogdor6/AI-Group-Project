using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alpaca : MonoBehaviour
{
    float speed = 10;

    public GameObject particles;
    
    public GameObject Alpaca;

    public AI_Scanner Scanner;

    bool gotHit = false;
    bool choseInput = false;

    public bool AI = false;
    public bool Testing = true;
    public bool UP = false;

    int input = 3;

    private float AIBulletOffSet = 0.5f;




    // Start is called before the first frame update
    void Start()
    {
        soundManager.soundsSingleton.startBackgroundSong("Blazer Rail");
    }

    // Update is called once per frame
    void Update()
    {
        if (!AI) { 

            switch (gotHit)
            {
                case false:
                    if (Input.GetKey(KeyCode.W))
                    {
                        Alpaca.transform.position += Vector3.up * speed * Time.deltaTime;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        Alpaca.transform.position += Vector3.down * speed * Time.deltaTime;
                    }
                    else if (Input.GetKeyDown("space"))
                    {
                        bulletManager.singleton.getBullet(new Vector3(Alpaca.gameObject.transform.position.x + AIBulletOffSet, Alpaca.gameObject.transform.position.y, 0));
                    }

                    //Don't let the alpaca go off the screen
                    Vector3 pos = Camera.main.WorldToViewportPoint(Alpaca.gameObject.transform.position);
                    pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
                    Alpaca.gameObject.transform.position = Camera.main.ViewportToWorldPoint(pos);
                    break;
            }

        }
        else // AI CONTROLS
        {

            //switch (gotHit)
           // {
              //  case false:


                    //Calculate AI input
                   // if (!choseInput && Scanner.checkQueue())
                   // {
                        input = Scanner.CalculateDecision();
                        //choseInput = true;
                    //}

                    //AI Deciding Movement

                    AIMovement(input);

                    //Don't let the alpaca go off the screen
                    Vector3 pos = Camera.main.WorldToViewportPoint(Alpaca.gameObject.transform.position);
                    pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
                    Alpaca.gameObject.transform.position = Camera.main.ViewportToWorldPoint(pos);


                    //AI Deciding Shooting

                    // AIShoot(1);

                   // break;

           // }


        }
    }

   public void AITakeAction()
    {
        input = Scanner.CalculateDecision();
        AIMovement(input);
    }


    public void AIMovement(int input)
    {
        //Input == 0 // Up

        //Input == 1 // Nothing

        //Input == 2 // Down

        switch (input)
        {
            case 0: 
                Alpaca.transform.position += Vector3.up * speed * Time.deltaTime;
                UP = true;
                break;
            case 1:
                bulletManager.singleton.getBullet(new Vector3(Alpaca.gameObject.transform.position.x + AIBulletOffSet, Alpaca.gameObject.transform.position.y, 0));
                break;
            case 2:
                Alpaca.transform.position += Vector3.down * speed * Time.deltaTime;
                UP = false;
                break;
            case 3:
                break;

        }

    }

    public void AIShoot(int input)
    {

        //if Input = 1 then shoot

        //Some choice to shoot

        if (input == 1)
        { //Shoot
            
        }
        else
        {

        }
    }


    public void playerDies()
    {
        if (!Testing) {

            if (gotHit != true)
            {
                if (!AI) { 
                    //Play animation
                    particles.GetComponent<ParticleSystem>().Play();
                    //Now block the thing
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                    points.singleton.stopTime();
                    tryAgain.singleton.bringUIDown();
                }



            }
            gotHit = true;

            //AITakeAction();
        }
    }

    public void resetChoice()
    {
        choseInput = false;
        gotHit = false;
    }

    public bool AITagged()
    {
        return gotHit;
    }

    public bool GetAIChoice()
    {
        return UP;
    }
}
