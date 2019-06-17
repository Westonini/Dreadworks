using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveUIWithParent : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        text.transform.position = namePos;
    }
}
