using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PresentMessage : MonoBehaviour
{
    public TextMeshProUGUI presentMessage;
    float fadeInOutTime = 0.1f;
    static PresentMessage instance = null;
    public float durationTime;

    float totalPresentCount = 0.0f;

    public static PresentMessage Instance
    {
        get
        {
            if (null == instance) instance = FindObjectOfType<PresentMessage>();
            return instance;
        }
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //totalPresentCount = 0;
        }
    }

    struct Present
    {
        public string msg;
        public float presentCount;
    }

    Queue<Present> presentQueue = new Queue<Present>();
    bool isPopUp;

    public void showMessage(string msg, float presentCount)
    {
        Present p;

        p.msg = msg;
        p.presentCount = presentCount;

        presentQueue.Enqueue(p);
        if(isPopUp == false)
        {
            StartCoroutine(showPresentQueue());
        }
    }

    public void OnPresentCollision()
    {
        float presentCount = 1.0f; // 증가할 presentCount 값
        totalPresentCount += presentCount;

        // presentCount 값을 showMessage 메서드에 전달하여 숫자를 화면에 보여줌
        showMessage("Present Collected! Count: " + totalPresentCount.ToString(), totalPresentCount);
    }

    IEnumerator showPresentQueue()
    {
        isPopUp = true;

        while(presentQueue.Count != 0)
        {
            Present p = presentQueue.Dequeue();
            yield return StartCoroutine(showMessageCorutine(p.msg, p.presentCount));
        }

        isPopUp = false;
    }

    IEnumerator showMessageCorutine(string msg, float presntCount)
    {
        presentMessage.text = msg;
        presentMessage.enabled = true;

        yield return fadeInOut(presentMessage, fadeInOutTime, true);

        float elapsedTime = 0.0f;
        while(elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(presentMessage, fadeInOutTime, false);

        presentMessage.enabled = false;
    }

    IEnumerator fadeInOut(TextMeshProUGUI target, float durationTime, bool inOut)
    {
        float start, end;
        if (inOut)
        {
            start = 0.0f;
            end = 0.5f;
        }
        else
        {
            start = 0.5f;
            end = 0f;
        }

        Color current = Color.clear; /* (0, 0, 0, 0) = 검은색 글자, 투명도 100% */
        float elapsedTime = 0.0f;

        while (elapsedTime < durationTime)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / durationTime);

            target.color = new Color(current.r, current.g, current.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
