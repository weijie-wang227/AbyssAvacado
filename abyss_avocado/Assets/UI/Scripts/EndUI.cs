using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    private string data;
    [SerializeField] private TMP_Text dataText;
    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject button;
    private float duration = 2f;

    void Start()
    {
        int killCount = RunStats.Instance.KillCount;
        int depth = RunStats.Instance.Depth;
        data = $"kills: {killCount}\ndepth: {depth}";
        StartCoroutine(Sequence());
    }

    // Update is called once per frame
    IEnumerator Sequence()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(TypeWriter());
        yield return new WaitForSeconds(2.5f);
        button.SetActive(true);
    }

    IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < 1.0f)
        {
            blackScreen.color = new Color(0f, 0f, 0f, 1 - timer / 0.5f);
            yield return null;
            timer += Time.deltaTime;
        }
        blackScreen.color = new Color(0f, 0f, 0f, 0);
    }

    IEnumerator TypeWriter()
    {
        float timer = 0;
        while (timer < duration)
        {
            dataText.text = data.Substring(0, (int)(timer / duration * (float)data.Length));
            yield return null;
            timer += Time.deltaTime;
        }
        dataText.text = data;
    }
}
