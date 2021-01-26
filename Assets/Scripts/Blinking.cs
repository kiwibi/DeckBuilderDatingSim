using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    SpriteRenderer arrowRenderer_;
    public float frequency_;
    public GameLogistics.GameState activeState_;
    private Color hidden;
    private Color shown;
    void Start()
    {
        arrowRenderer_ = GetComponent<SpriteRenderer>();
        shown = arrowRenderer_.color;
        hidden = new Color(arrowRenderer_.color.r, arrowRenderer_.color.g, arrowRenderer_.color.b, 0);
    }   
    IEnumerator blink()
    {

        while(activeState_ == GameLogistics.GetCurrentState() && gameObject.activeSelf)
        {
            float tmpFrequency;
            if (arrowRenderer_.color == shown){
                tmpFrequency = frequency_;
            }
            else{
                tmpFrequency = frequency_ / 3;
            }
            yield return new WaitForSeconds(tmpFrequency);
            if(arrowRenderer_.color == shown) {
                arrowRenderer_.color = hidden;
            }else{
                arrowRenderer_.color = shown;
            }
        }
    }
    public void ActivateArrow()
    {
        StartCoroutine(blink());
    }
}
