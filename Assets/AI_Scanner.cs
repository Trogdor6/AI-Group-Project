using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Scanner : MonoBehaviour
{

    public Queue<GameObject> incoming; //Queue of incoming walls
    

    public alpaca AGENT; // The AI Agent

    bool DQ = false; 

    float rewardAmount = 0.05f;    //How much the AI changes per Interaction
    float rewardMultiplier = 3.0f;  //A reward for picking the good option.

  

    // Start is called before the first frame update
    void Start()
    {
        incoming = new Queue<GameObject>();

    }



    // Update is called once per frame
    void Update()
    {
        if(incoming.Count > 0 && incoming.Peek() == AGENT && !DQ) //Remove alpaca from Queue oops.
        {
            incoming.Dequeue();
            DQ = true;
            //Debug.Log(incoming.Count);
        }
        else
        {

        }
    }

    public void addToQueue(GameObject i) //Adds Walls to the Queue
    {
        incoming.Enqueue(i);
        //AGENT.AITakeAction();
    }

    public void removeFromQueue() //Removes Walls from the queue
    {
        incoming.Dequeue();
        
    }

    void OnTriggerEnter2D(Collider2D collision) //Add incoming walls to queue when they enter the scene
    {
        if(collision.gameObject.tag != "Player")
        {
            incoming.Enqueue(collision.gameObject);



           // Debug.Log("Queued", collision.gameObject);
           // Debug.Log(collision.gameObject);

        }
        
    }

    void OnTriggerExit2D(Collider2D other)//Remove past walls from queue
    {
        string wallType = incoming.Peek().gameObject.name;


        //This large chunk of code, checks whether the AI was hit recently, (i.e the wall has passed the AI)
        //if not, it rewards the AI for choosing a good option for that specific wall type. Otherwise it gives a penalty.

        if (!AGENT.AITagged()) // We didn't get Hit
        {
            print("PASSED: " + wallType);

            if (wallType == "WallA(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallA_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallA_MoveUp") + rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }
                else //DOWN **
                {
                    PlayerPrefs.SetFloat("WallA_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallA_MoveUp") - (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //Double Reward
                    PlayerPrefs.Save();
                }
                print("A:" + PlayerPrefs.GetFloat("WallA_MoveUp"));
            }

            if (wallType == "WallB(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP **
                {
                    PlayerPrefs.SetFloat("WallB_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallB_MoveUp") + (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //Double Reward
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallB_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallB_MoveUp") - rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }
                print("B:" + PlayerPrefs.GetFloat("WallB_MoveUp"));
            }

            if (wallType == "WallC(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP **
                {
                    PlayerPrefs.SetFloat("WallC_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallC_MoveUp") + (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //Double Reward
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallC_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallC_MoveUp") - rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }

                print("C:" + PlayerPrefs.GetFloat("WallC_MoveUp"));
            }

            if (wallType == "WallD(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP **
                {
                    PlayerPrefs.SetFloat("WallD_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallD_MoveUp") + rewardAmount, 0.00f, 1.00f)); 
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallD_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallD_MoveUp") - (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //Double Reward
                    PlayerPrefs.Save();
                }

                print("D:" + PlayerPrefs.GetFloat("WallD_MoveUp"));
            }

            if (wallType == "WallE(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallE_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallE_MoveUp") + rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallE_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallE_MoveUp") - rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }

                print("E:" + PlayerPrefs.GetFloat("WallE_MoveUp"));
            }


        }
        else // We got Hit
        {
            print("FAILED: " + wallType);

            if (wallType == "WallA(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {   
                    PlayerPrefs.SetFloat("WallA_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallA_MoveUp") - (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //PENALTY
                    PlayerPrefs.Save();
                }
                else //DOWN **
                {
                    PlayerPrefs.SetFloat("WallA_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallA_MoveUp") + rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }
                print("A:" + PlayerPrefs.GetFloat("WallA_MoveUp"));
            }

            if (wallType == "WallB(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallB_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallB_MoveUp") - rewardAmount, 0.00f, 1.00f)); 
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallB_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallB_MoveUp") + (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //PENALTY
                    PlayerPrefs.Save();
                }

                print("B:" + PlayerPrefs.GetFloat("WallB_MoveUp"));
            }

            if (wallType == "WallC(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallC_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallC_MoveUp") - rewardAmount, 0.00f, 1.00f)); 
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallC_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallC_MoveUp") + (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //PENALTY
                    PlayerPrefs.Save();
                }

                print("C:" + PlayerPrefs.GetFloat("WallC_MoveUp"));
            }

            if (wallType == "WallD(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallD_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallD_MoveUp") - (rewardAmount * rewardMultiplier), 0.00f, 1.00f)); //Penalty
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallD_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallD_MoveUp") + rewardAmount, 0.00f, 1.00f)); 
                    PlayerPrefs.Save();
                }

                print("D:" + PlayerPrefs.GetFloat("WallD_MoveUp"));
            }

            if (wallType == "WallE(Clone)")
            {
                if (AGENT.GetAIChoice()) // UP
                {
                    PlayerPrefs.SetFloat("WallE_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallE_MoveUp") - rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }
                else //DOWN
                {
                    PlayerPrefs.SetFloat("WallE_MoveUp", Mathf.Clamp(PlayerPrefs.GetFloat("WallE_MoveUp") + rewardAmount, 0.00f, 1.00f));
                    PlayerPrefs.Save();
                }

                print("E:" + PlayerPrefs.GetFloat("WallE_MoveUp"));
            }

        }

       
        // The Agent is now considered 'not hit' and can calculate it's next move.

        AGENT.resetChoice();


        //Remove the passed Wall from the queue as it is no longer needed.

        incoming.Dequeue();
        //Debug.Log("Dequeued");
    }

    public void SetDefaultProbabilities() //Set default probabilities of AI
    {
        
        //Wall A
        PlayerPrefs.SetFloat("WallA_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallB_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallC_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallD_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallE_MoveUp", 0.5f);


        PlayerPrefs.Save();

        //Debug.Log(PlayerPrefs.GetFloat("Above_MoveUp"));
    }

    public void deleteProbabilities() //Completely Erases All Learned Data
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("WallA_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallB_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallC_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallD_MoveUp", 0.5f);

        PlayerPrefs.SetFloat("WallE_MoveUp", 0.5f);

        PlayerPrefs.Save();
    }

    public int CalculateDecision()
    {
        float AI_Pos = AGENT.transform.position.y;

        //Check if incoming is a WALL or an ENEMY
        if (incoming.Count > 0 && incoming.Peek().gameObject.name == "enemy") //ENEMY
        {
            float Enemy_Pos = incoming.Peek().gameObject.transform.position.y;

            if (AI_Pos <= Enemy_Pos + 0.5f || AI_Pos >= Mathf.Abs(Enemy_Pos) - 0.5f)
            {
                return MakeChoice("Even");
            }

            // Debug.Log("Enemy");
            if (AI_Pos > Enemy_Pos)
            {
                return MakeChoice("Above");
            }

            if (AI_Pos < Enemy_Pos)
            {
                return MakeChoice("Below");
            }

           

            return 3;

        } //Enemy is not implemented
        else // WALLS
        {
            if (incoming.Count > 0)
            {
                GameObject wall = incoming.Peek(); 


                // The AI considers the type of wall that is approaching and uses it's appropriate probabilities.

                if(wall.name == "WallA(Clone)")
                {
                    //Debug.Log("A");

                    return MakeChoice("A");
                }

                if (wall.name == "WallB(Clone)")
                {
                    //Debug.Log("B");
                    return MakeChoice("B");
                }

                if (wall.name == "WallC(Clone)")
                {
                    // Debug.Log("C");
                    return MakeChoice("C");
                }

                if (wall.name == "WallD(Clone)")
                {
                    // Debug.Log("D");
                    return MakeChoice("D");
                }

                if (wall.name == "WallE(Clone)")
                {
                    // Debug.Log("E");
                    return MakeChoice("E");
                }

                return 3;

            }

            return 3;






        }
    }// Calculate Decisions.. END


    public int MakeChoice(string wallType) 
    {

        //This function takes the percentages of the appropriate wall and returns an Action for the AI to take.
        // It returns 0 for UP and 2 for DOWN

        float rNum = Random.Range(0.0f, 100.0f);

        if (wallType == "A" && rNum < PlayerPrefs.GetFloat("WallA_MoveUp") * 100)
        {

            return 0;
        }
        else if (wallType == "A" && rNum > PlayerPrefs.GetFloat("WallA_MoveUp") * 100)
        {
            return 2;
        }



        if (wallType == "B" && rNum < PlayerPrefs.GetFloat("WallB_MoveUp") * 100)
        {

            return 0;
        }
        else if (wallType == "B" && rNum > PlayerPrefs.GetFloat("WallB_MoveUp") * 100)
        {
            return 2;
        }



        if (wallType == "C" && rNum < PlayerPrefs.GetFloat("WallC_MoveUp") * 100)
        {

            return 0;
        }
        else if (wallType == "C" && rNum > PlayerPrefs.GetFloat("WallC_MoveUp") * 100)
        {
            return 2;
        }



        if (wallType == "D" && rNum < PlayerPrefs.GetFloat("WallD_MoveUp") * 100)
        {

            return 0;
        }
        else if (wallType == "D" && rNum > PlayerPrefs.GetFloat("WallD_MoveUp") * 100)
        {
            return 2;
        }



        if (wallType == "E" && rNum < PlayerPrefs.GetFloat("WallE_MoveUp") * 100)
        {

            return 0;
        }
        else if (wallType == "E" && rNum > PlayerPrefs.GetFloat("WallE_MoveUp") * 100)
        {
            return 2;
        }



        return 3;
    }

    public bool checkQueue() //Check if anything is in the queue. (No walls?)
    {
        if (incoming.Count > 0)
        {
            return true;
        }
        else
            return false;
    }

}
