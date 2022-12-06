using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacingAdjust : MonoBehaviour
{
    //private HorizontalLayoutGroup layoutGroup;
    private GridLayoutGroup layoutGroup;
    private int amountToReadjust = -4;
    private int cardsUntilReadjust = 8;
    private int minSpacing = -60;
    
    void Start()
    {
        //layoutGroup = GetComponent<HorizontalLayoutGroup>();
        layoutGroup = GetComponent<GridLayoutGroup>();
        StartCoroutine("ReadjustSpacing");
    }

    
    private IEnumerator ReadjustSpacing(){
        int numberOfChildren = transform.childCount;
        if(numberOfChildren > cardsUntilReadjust){
            //layoutGroup.spacing = (numberOfChildren - cardsUntilReadjust) * amountToReadjust;
            layoutGroup.spacing = new Vector2((numberOfChildren - cardsUntilReadjust) * amountToReadjust, layoutGroup.spacing.y);
            layoutGroup.spacing = layoutGroup.spacing.x <= minSpacing ? new Vector2(minSpacing, layoutGroup.spacing.y) : layoutGroup.spacing;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("ReadjustSpacing");
    }
}
