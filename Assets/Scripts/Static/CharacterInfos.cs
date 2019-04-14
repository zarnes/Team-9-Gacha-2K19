using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterInfos {
    public static List<FollowerBehavior> puppets = new List<FollowerBehavior>();
    public static int puppetIndex;

    static CharacterInfos() {
        puppets = new List<FollowerBehavior>();
        GameObject[] go = GameObject.FindGameObjectsWithTag("puppet");
        
        foreach(GameObject tpGo in go) {
            puppets.Add(tpGo.GetComponent<FollowerBehavior>());
        }
    }

    public static void NextPuppet() {
        if(puppetIndex == puppets.Count - 1) {
            puppetIndex = 0;
        } else {
            puppetIndex++;
        }
        for(int i = 0; i < puppets.Count; i++) {
            if (i != puppetIndex) {
                puppets[i].Unpossess(puppets[puppetIndex].transform);
            }
            else
                puppets[i].Possess();
        }
        Debug.Log("[Static] Current puppet : " + puppetIndex);
    }
}
