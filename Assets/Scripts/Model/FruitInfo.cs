using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhoWhenLone;

public class FruitInfo : MonoBehaviour
{
    //[HideInInspector]
    public DataDef.FruitType fruitType;

    public DataDef.FruitState fruitState = DataDef.FruitState.Ready;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        fruitState = DataDef.FruitState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fruitState == DataDef.FruitState.Ready)
        {
            fruitState = DataDef.FruitState.StandBy;
        }

        if (fruitState == DataDef.FruitState.StandBy && Input.GetMouseButtonUp(0) && GameManager.GetInstance().gameState == DataDef.GameState.StandBy)
        {
            GameManager.GetInstance().gameState = DataDef.GameState.InProgress;
            fruitState = DataDef.FruitState.Drop;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            Invoke("InvokeCreateFruit", 0.3f);
        }

        if (GameManager.GetInstance().gameState == DataDef.GameState.StandBy && fruitState == DataDef.FruitState.StandBy)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        }
    }

    void InvokeCreateFruit()
    {
        Debug.Log("InvokeCreateFruit");
        GameObject fruit = GameManager.GetInstance().CreateFruit(DataDef.CreateType.Create);
    }

    void OnCollisionEnter2D(Collision2D collision2d)
    {
        GameObject colliderFruit = collision2d.gameObject;
        if ((colliderFruit.tag.Contains("Wall") || colliderFruit.tag.Contains("Fruit")) && fruitState == DataDef.FruitState.Drop)
        {
            if (colliderFruit.tag == "Wall")
            {
                GameManager.GetInstance().PlayDropToWallSound();
            }

            fruitState = DataDef.FruitState.Collision;
        }

        GameManager.GetInstance().gameState = DataDef.GameState.StandBy; 

        FruitInfo fruitInfo = colliderFruit.GetComponent<FruitInfo>();
        if (fruitInfo != null && colliderFruit.GetComponent<FruitInfo>().fruitType == this.fruitType 
                              && colliderFruit.GetComponent<FruitInfo>().fruitState == DataDef.FruitState.Collision)
        {
            if (transform.position.y > colliderFruit.transform.position.y)
            {
                CompressFruit();
                Destroy(colliderFruit);
                Destroy(this.gameObject);
            }
        }
    }

    private void CompressFruit()
    {
        fruitType = fruitType + 1;
        Debug.Log("CompressFruit" + fruitType);
        GameObject fruit = GameManager.GetInstance().CreateFruit(DataDef.CreateType.Compress, fruitType);
        fruit.transform.localPosition = transform.localPosition;
        fruit.transform.localScale = Vector3.one;
        fruit.GetComponent<Rigidbody2D>().gravityScale = 1;
        fruit.GetComponent<FruitInfo>().fruitState = DataDef.FruitState.Collision;
        GameManager.GetInstance().PlayCompressSound();

        GameManager.GetInstance().AddScore((int)fruitType);
    }
}
