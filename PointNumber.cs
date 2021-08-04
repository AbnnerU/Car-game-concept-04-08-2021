using UnityEngine;
using UnityEngine.UI;

public class PointNumber : MonoBehaviour
{
    [SerializeField] private Text canvasText;

    private void OnEnable()
    {
        canvasText.enabled = false;
    }

    public void SetPointNumber(int number)
    {
        canvasText.enabled = true;

        canvasText.text = number + "";
    }
}
