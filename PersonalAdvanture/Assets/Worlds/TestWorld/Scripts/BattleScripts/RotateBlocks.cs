using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlocks : MonoBehaviour
{
    public GameObject[] blocks;
    Vector3[] blockPositions;
    BattleManager bScript;
    public int blockStaging;
    float delayTimer;
    public float delay;
    public float speed;
    public float end;

    //Green - Slingshot
    //Yellow - Item
    //Red - Pounch
    //Blue - Runaway

    // Use this for initialization
    void Start()
    {
        blockPositions = new Vector3[4];
        blockStaging = 1;
        delayTimer = 10;
        bScript = GetComponent<BattleManager>();
        blocks = GameObject.FindGameObjectsWithTag("BattleBlock");
        for (int i = 0; i < 4; i++)
        {
            blockPositions[i] = blocks[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bScript.stages == 1)
        {
            foreach (GameObject block in blocks)
            {
                block.SetActive(true);
            }

            if (Input.GetAxis("MenuHorizontal") < 0 && delayTimer >= delay)
            {
                blockStaging--;
                delayTimer = 0;
            }

            if (Input.GetAxis("MenuHorizontal") > 0 && delayTimer >= delay)
            {
                blockStaging++;
                delayTimer = 0;
            }

            delayTimer += 1 * Time.deltaTime;
            if (delayTimer > delay + 1)
                delayTimer = delay + 1;

            if (blockStaging > 4)
            {
                blockStaging = 1;
            }
            if (blockStaging < 1)
            {
                blockStaging = 4;
            }

            //SlingShot
            if (blockStaging == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                   moveBlock(blocks[i], blockPositions[i]);                 
                }
            }
            //Item
            if (blockStaging == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    moveBlock(blocks[i], blockPositions[i + 1]);                  
                }
                moveBlock(blocks[3], blockPositions[0]);
            }

            //Runaway
            if (blockStaging == 3)
            {
                moveBlock(blocks[2], blockPositions[0]);
                moveBlock(blocks[3], blockPositions[1]);
                moveBlock(blocks[0], blockPositions[2]);
                moveBlock(blocks[1], blockPositions[3]);            
            }

            //Pounch
            if (blockStaging == 4)
            {
                for (int i = 1; i < 4; i++)
                {
                    moveBlock(blocks[i], blockPositions[i - 1]);
                }
                moveBlock(blocks[0], blockPositions[3]);
            }

        }
        if (bScript.stages > 2)
        {
            foreach(GameObject block in blocks)
            {
                block.SetActive(false);
            }
        }

        }
    public void moveBlock(GameObject actionBlock, Vector3 targetPosition)
    {
        //check out using slerps/lerps to make this smoother
        Vector3 currentPosition = actionBlock.transform.position;
        Vector3 newPos = Vector3.Lerp(actionBlock.transform.position, targetPosition, 0.08f);
        actionBlock.transform.position = newPos;

    }
}
