using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToEnable;
    public AudioSource buttonsound;
    public void OnPointerEnter(PointerEventData eventData)
    {
        objectToEnable.SetActive(true);
        buttonsound.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objectToEnable.SetActive(false);
    }
}
