using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectorLvl : MonoBehaviour
{
    public GameObject bot;
    Text i;
    Component[] BehaviorTrs;

    private void Start()
    {
        i = GetComponent<Text>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0f);
        BehaviorTrs = bot.GetComponents<BehaviorTree>();
        Debug.Log(BehaviorTrs[1]);
        Debug.Log(BehaviorTrs[0]);
        Debug.Log(BehaviorTrs[2]);

    }



    // can ignore the update, it's just to make the coroutines get called for example
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            i.text = "CPU LEVEL: EASY";
            StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
            Debug.Log(BehaviorTrs[1]);
            bot.GetComponents<BehaviorTree>()[1].enabled = true;
            bot.GetComponents<BehaviorTree>()[0].enabled = false;
            bot.GetComponents<BehaviorTree>()[2].enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            i.text = "CPU LEVEL: NORMAL";
            StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
            bot.GetComponents<BehaviorTree>()[1].enabled = false;
            bot.GetComponents<BehaviorTree>()[2].enabled = true;
            bot.GetComponents<BehaviorTree>()[0].enabled = false;
            Debug.Log(BehaviorTrs[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            i.text = "CPU LEVEL: HARD AS BALLS";
            StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
            bot.GetComponents<BehaviorTree>()[0].enabled = true;
            bot.GetComponents<BehaviorTree>()[2].enabled = false;
            bot.GetComponents<BehaviorTree>()[1].enabled = false;
            Debug.Log(BehaviorTrs[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            i.text = "CPU LEVEL:  STOP";
            StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
            bot.GetComponents<BehaviorTree>()[0].enabled = false;
            bot.GetComponents<BehaviorTree>()[2].enabled = false;
            bot.GetComponents<BehaviorTree>()[1].enabled = false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(0);

        }

    }



    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}