using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Management")]
    public int sceneToLoad;
    public GameObject aviso;

    public void Cambio()
    {
        LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cambio();
        }
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Aviso()
    {
        StartCoroutine(Avisar());
    }

    IEnumerator Avisar()
    {
        aviso.SetActive(true);
        yield return new WaitForSeconds(20);
        aviso.SetActive(false);
        yield return null;
    }
}
