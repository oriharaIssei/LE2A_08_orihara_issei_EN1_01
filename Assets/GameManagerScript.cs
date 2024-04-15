using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    int[] map;
    int playerIndex = 1;

    void PrintIntArray(int[] intArray) {
        string debugText = "";
        for (int i = 0; i < intArray.Length; ++i) {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    // Start is called before the first frame update
    void Start() {
        map = new int[] { 0, 1, 0, 0, 0, 0 };

        PrintIntArray(map);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (playerIndex > 0) {
                map[playerIndex - 1] = 1;
                map[playerIndex] = 0;
                --playerIndex;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (playerIndex < map.Length - 1) {
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
                ++playerIndex;
            }
        }
        PrintIntArray(map);
    }
}
