using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WhoWhenLone
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject initPos;
        public Transform parent;
        public DataDef.GameState gameState = DataDef.GameState.Ready;
        public AudioSource audio;
        private int score;
        public Text scoreText;
        public static GameManager GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            GameObject fruit = CreateFruit(DataDef.CreateType.Create);
            gameState = DataDef.GameState.StandBy;
        }

        // Update is called once per frame
        void Update()
        {
//            if (Input.GetKeyDown(KeyCode.A))
//            {
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                if (Physics.Raycast(ray, out RaycastHit hitInfo))
//                {
//                    Debug.Log(hitInfo.collider.gameObject.name);
//                    Debug.Log(hitInfo.collider.gameObject.GetComponent<FruitInfo>().fruitState);
//                }
//            }
            if (Input.GetMouseButtonDown(2))
            {
                Debug.Log("mouse down");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isHit = Physics.Raycast(ray, out hitInfo);
                Debug.Log("isHit = "+ isHit);

                EventSystem eventSystem = EventSystem.current;
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> list = new List<RaycastResult>();

                eventSystem.RaycastAll(pointerEventData, list);
                if (list.Count > 0)
                {
                    Debug.Log(list[0].gameObject.name);
                }
            }
            
        }

        public GameObject CreateFruit(DataDef.CreateType createType, DataDef.FruitType type = DataDef.FruitType.None)
        {
            if (type == DataDef.FruitType.None)
            {
                int value = Random.Range(1, DataDef.MaxCreateFruitType);
                type = (DataDef.FruitType)value;
            }

            string path = DataDef.FruitPrefabPath[type];
            GameObject fruit = (GameObject)Instantiate(Resources.Load(path), parent.transform);
            if (createType == DataDef.CreateType.Create)
            {
                fruit.transform.localPosition = new Vector3(0, 880, 0);
                fruit.transform.localScale = Vector3.one;
            }

            return fruit;
        }

        public void PlayDropToWallSound()
        {
            audio.clip = (AudioClip)Resources.Load("Sound/Drop");
            audio.Play();
        }

        public void PlayCompressSound()
        {
            audio.clip = (AudioClip)Resources.Load("Sound/Compress");
            audio.Play();
        }

        public void AddScore(int addScore)
        {
            score = score + addScore;
            scoreText.text = score.ToString();
        }
    }
}

