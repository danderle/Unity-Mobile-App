using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler 
{
    private Image mBackgroundImage;
    private Image mJoystickImage;

    public Vector3 InputDirection { get; set; }

    private void Start()
    {
        mBackgroundImage = GetComponent<Image>();
        mJoystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mBackgroundImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / mBackgroundImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / mBackgroundImage.rectTransform.sizeDelta.y);

            float x = (mBackgroundImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (mBackgroundImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            mJoystickImage.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (mBackgroundImage.rectTransform.sizeDelta.x / 3), InputDirection.z * (mBackgroundImage.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        mJoystickImage.rectTransform.anchoredPosition = InputDirection;
    }
}
