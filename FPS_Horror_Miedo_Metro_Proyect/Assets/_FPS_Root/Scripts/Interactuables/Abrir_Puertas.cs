using UnityEngine;
using TMPro;

public class Abrir_Puertas : MonoBehaviour
{
    public TMP_InputField inputField;
    public string accesKey;

    void Start()
    {
        inputField.onEndEdit.AddListener(CheckKey);
    }

    void CheckKey(string input)
    {
        if (input == accesKey)
        {
            Debug.Log("Clave correcta");
        }
        else
        {
            Debug.Log("Clave incorrecta");
        }
    }
}
