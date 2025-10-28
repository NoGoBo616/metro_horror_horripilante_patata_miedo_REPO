using UnityEngine;
using TMPro;

public class Abrir_Puertas : MonoBehaviour
{
    public TMP_InputField inputField;
    public string accesKey;
    public Animator anim;

    void Start()
    {
        inputField.onEndEdit.AddListener(CheckKey);
    }

    void CheckKey(string input)
    {
        if (input == accesKey)
        {
            Debug.Log("Clave correcta");
            anim.SetTrigger("Open");
        }
        else
        {
            Debug.Log("Clave incorrecta");
        }
    }
}
