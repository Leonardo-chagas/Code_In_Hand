using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortChildren : MonoBehaviour
{
    
    //rearranges children of object based on the bubble sort algorithm
    public void SortChildrenByPosition(){
        List<Transform> children = new List<Transform>();
        foreach(Transform child in transform){
            children.Add(child);
        }

        int i, j;
        int n = children.Count;
        for(i = 0; i < n - 1; i++){
            for(j = 0; j < n - i - 1; j++){
                if(children[j].position.y < children[j + 1].position.y || 
                (children[j].position.y == children[j + 1].position.y && children[j].position.x > children[j + 1].position.x)){
                    children[j].SetSiblingIndex(j + 1);
                }
            }
        }
    }
}
