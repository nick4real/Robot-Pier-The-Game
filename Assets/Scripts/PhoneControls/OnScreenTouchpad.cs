using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

[AddComponentMenu("Input/On-Screen Touchpad")]
public class OnScreenTouchpad : OnScreenControl, IDragHandler
{
    [SerializeField] private PlayerView playerView;
    public void OnDrag(PointerEventData eventData)
    {
        playerView.Look(eventData.delta);
    }

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_controlPath;
    protected override string controlPathInternal
    {
        get => m_controlPath;
        set => m_controlPath = value;
    }
}
