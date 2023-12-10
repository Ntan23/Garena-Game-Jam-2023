using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public void HoverFX() => LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
    public void UnHoverFX() => LeanTween.scale(gameObject, Vector3.one, 0.3f);
}
