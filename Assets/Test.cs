using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitFiveSeconds());
        SceneManager.LoadScene("VN");
    }

    IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(2);
    }
}
