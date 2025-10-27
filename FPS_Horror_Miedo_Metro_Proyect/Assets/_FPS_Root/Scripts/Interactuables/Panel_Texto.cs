using UnityEngine;
using TMPro;

public class Panel_Texto : MonoBehaviour
{
    public TMP_InputField inputField;

    void OnEnable()
    {
        StartCoroutine(ActivateInput());
    }

    private void OnDisable()
    {
        inputField.text = string.Empty;
    }

    private System.Collections.IEnumerator ActivateInput()
    {
        yield return null;
        inputField.Select();
        inputField.ActivateInputField();
    }
}
