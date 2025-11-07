using UnityEngine;

public class Material_Change : MonoBehaviour
{
    [Header("Materiales posibles (array)")]
    public Material[] materials; // Lista de materiales posibles

    [Header("Renderer del modelo (opcional)")]
    public Renderer targetRenderer;

    void Awake()
    {
        // Si no se asigna un Renderer manualmente, usa el del mismo objeto
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        if (materials == null || materials.Length == 0 || targetRenderer == null)
        {
            Debug.LogWarning("Faltan materiales o renderer en " + gameObject.name);
            return;
        }

        // Escoge un material aleatorio
        Material randomMaterial = materials[Random.Range(0, materials.Length)];

        // Asigna el material al modelo
        targetRenderer.material = randomMaterial;
    }
}
