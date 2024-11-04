using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{

    // Update is called once per frame
    public void OnClick()
    {
        Debug.Log("start");
        SceneManager.LoadScene("abyss");
    }
}
