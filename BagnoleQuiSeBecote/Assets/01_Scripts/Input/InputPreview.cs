using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InputPreview : MonoBehaviour
{
    [SerializeField] private RectTransform blueCursor, redCursor;
    private float blueCursorValue, redCursorValue;

    private void Update()
    {
        blueCursorValue = Mathf.Lerp(blueCursorValue, InputHandler.Instance.J1Input.turn * 500, 8 * Time.deltaTime);
        redCursorValue = Mathf.Lerp(redCursorValue, InputHandler.Instance.J2Input.turn * 500, 8 * Time.deltaTime);
        
        blueCursor.localPosition = new Vector3(blueCursorValue, blueCursor.localPosition.y, 0);
        redCursor.localPosition = new Vector3(redCursorValue, redCursor.localPosition.y, 0);
    }
}
