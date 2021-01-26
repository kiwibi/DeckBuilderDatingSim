using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
    SpriteRenderer[] childRenderers;

    private Color hidden;
    private Color shown;

    void Start()
    {
        childRenderers = GetComponentsInChildren<SpriteRenderer>();
        shown = childRenderers[0].color;
        hidden = new Color(childRenderers[0].color.r, childRenderers[0].color.g, childRenderers[0].color.b, 0);
    }
    public void setNumber(int number)
    {
        hideNumbers();
        switch (number)
        {
            case 0:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[2].color = shown;
                childRenderers[4].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
            case 1:
                childRenderers[2].color = shown;
                childRenderers[5].color = shown;
                break;
            case 2:
                childRenderers[0].color = shown;
                childRenderers[2].color = shown;
                childRenderers[3].color = shown;
                childRenderers[4].color = shown;
                childRenderers[6].color = shown;
                break;
            case 3:
                childRenderers[0].color = shown;
                childRenderers[2].color = shown;
                childRenderers[3].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
            case 4:
                childRenderers[1].color = shown;
                childRenderers[2].color = shown;
                childRenderers[3].color = shown;
                childRenderers[5].color = shown;
                break;
            case 5:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[3].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
            case 6:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[3].color = shown;
                childRenderers[4].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
            case 7:
                childRenderers[0].color = shown;
                childRenderers[2].color = shown;
                childRenderers[5].color = shown;
                break;
            case 8:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[2].color = shown;
                childRenderers[3].color = shown;
                childRenderers[4].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
            case 9:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[2].color = shown;
                childRenderers[3].color = shown;
                childRenderers[5].color = shown;
                break;
            default:
                childRenderers[0].color = shown;
                childRenderers[1].color = shown;
                childRenderers[2].color = shown;
                childRenderers[4].color = shown;
                childRenderers[5].color = shown;
                childRenderers[6].color = shown;
                break;
        }
    }

    private void hideNumbers()
    {
        foreach(var renderer in childRenderers)
        {
            renderer.color = hidden;
        }
    }
}


