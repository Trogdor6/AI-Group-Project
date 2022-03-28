using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Scanner : MonoBehaviour
{

    public Queue<GameObject> incoming;
    public Stack<int> LastDecision;

    public GameObject alpaca;

    bool DQ = false;

    public enum Gaps { TOP, BOT, BOTH }

    // Start is called before the first frame update
    void Start()
    {
        incoming = new Queue<GameObject>();

    }



    // Update is called once per frame
    void Update()
    {
        if(incoming.Count > 0 && incoming.Peek() == alpaca && !DQ) //Remove alpaca from Queue oops.
        {
            incoming.Dequeue();
            DQ = true;
            Debug.Log(incoming.Count);
        }
        else
        {

        }
    }

    public void addToQueue(GameObject i)
    {
        incoming.Enqueue(i);
    }

    public void removeFromQueue()
    {
        incoming.Dequeue();
    }

    void OnTriggerEnter2D(Collider2D collision) //Add incoming walls to queue
    {
        incoming.Enqueue(collision.gameObject);

        //Debug.Log("Queued", collision.gameObject);
        //Debug.Log(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)//Remove past walls from queue
    {
        incoming.Dequeue();

        //Debug.Log("Dequeued");
    }

    public void SetDefaultProbabilities() //Set default probabilities of AI
    {
        
        //Wall A
        PlayerPrefs.SetFloat("WallA_Above_MoveUp", 0.33f);
        PlayerPrefs.SetFloat("WallA_Above_MoveDown", 0.33f);
        PlayerPrefs.SetFloat("WallA_Above_Shoot", 0.33f);

        PlayerPrefs.SetFloat("WallA_Below_MoveUp", 0.33f);
        PlayerPrefs.SetFloat("WallA_Below_MoveDown", 0.33f);
        PlayerPrefs.SetFloat("WallA_Below_Shoot", 0.33f);

        PlayerPrefs.SetFloat("WallA_Even_MoveUp", 0.33f);
        PlayerPrefs.SetFloat("WallA_Even_MoveDown", 0.33f);
        PlayerPrefs.SetFloat("WallA_Even_Shoot", 0.33f);


        PlayerPrefs.Save();

        //Debug.Log(PlayerPrefs.GetFloat("Above_MoveUp"));
    }

    public void deleteProbabilities()
    {
        PlayerPrefs.DeleteAll();
    }

    public int CalculateDecision()
    {
        float AI_Pos = alpaca.transform.position.y;

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

        }
        else // WALLS
        {
            if (incoming.Count > 0)
            {
                GameObject wall = incoming.Peek();

                float wallMaxY = wall.gameObject.transform.position.y + wall.gameObject.transform.localScale.y / 2;
                float wallMinY = wall.gameObject.transform.position.y - wall.gameObject.transform.localScale.y / 2;

                //float TopGap = 5.0f - wallMaxY;
                //float BotGap = -4.8f - wallMinY;

                //if (TopGap > .5)
                //{
                //    Debug.Log("Top Gap");
                //}
                //else if (BotGap < -4.3)
                //{
                //    Debug.Log("Bot Gap");
                //}
                //else if (TopGap > .5 && BotGap < -4.3)
                //{
                //    Debug.Log("Both");
                //
                //}

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
        int rNum = Random.Range(1, 100);

        if (wallType == "A" && rNum < 70)
        {
            return 2;
        }
        else if(wallType == "A" && rNum > 70)
        {
            return 0;
        }

        if (wallType == "B" && rNum < 70)
        {
            return 0;
        }
        else if (wallType == "B" && rNum > 70)
        {
            return 2;
        }

        if (wallType == "C" && rNum < 70)
        {
            return 0;
        }
        else if (wallType == "C" && rNum > 70)
        {
            return 2;
        }

        if (wallType == "D" && rNum < 70)
        {
            return 2;
        }
        else if (wallType == "D" && rNum > 70)
        {
            return 0;
        }

        if (wallType == "E" && rNum < 70)
        {
            return 2;
        }
        else if (wallType == "E" && rNum > 70)
        {
            return 0;
        }

        if (wallType == "Even")
        {
            return 1;
        }

        if (wallType == "Above")
        {
            return 2;
        }

        if (wallType == "Below")
        {
            return 0;
        }

        




        return 3;
    }

}
