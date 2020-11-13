using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInputTouchHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image _inputJoystickBase;
    [SerializeField]
    private Image _inputDirectionPoint;

    private bool _shown = false;

    private void Start()
    {
        HideInputJoystick();
    }

    private void ShowInputJoystick()
    {
        _inputJoystickBase.enabled = true;
        _inputDirectionPoint.enabled = true;
    }

    private void HideInputJoystick()
    {
        _inputJoystickBase.enabled = false;
        _inputDirectionPoint.enabled = false;
    }

    private void SetJoystickBasePosition(Vector2 position)
    {
        _inputJoystickBase.transform.position = position;
    }

    private void SetInputDirectionPointPosition(Vector2 position)
    {
        _inputDirectionPoint.transform.position = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ShowInputJoystick();
        SetInputDirectionPointPosition(eventData.position);
        SetJoystickBasePosition(eventData.position);
        _shown = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetInputDirectionPointPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HideInputJoystick();
    }
}
