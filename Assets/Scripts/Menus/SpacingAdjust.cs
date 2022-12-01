using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacingAdjust : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    private int amountToReadjust = -5;
    private int cardsUntilReadjust = 8;
    
    void Start()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        StartCoroutine("ReadjustSpacing");
    }

    
    private IEnumerator ReadjustSpacing(){
        int numberOfChildren = transform.childCount;
        if(numberOfChildren > cardsUntilReadjust){
            layoutGroup.spacing = (numberOfChildren - cardsUntilReadjust) * amountToReadjust;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("ReadjustSpacing");
    }
}
