using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    public Text m_textComponent;

    float m_acumTime = 0;
    float m_timeToDisable;
    public void SetMessage(string text, float time)
    {
        gameObject.SetActive(true);
        m_acumTime = 0;
        m_textComponent.text = text;
        m_timeToDisable = time;
    }

    public void Update()
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > m_timeToDisable)
        {
            gameObject.SetActive(false);
        }
    }


}
