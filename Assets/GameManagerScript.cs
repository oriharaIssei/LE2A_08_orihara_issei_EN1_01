using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.SearchService;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject boxPrefab;

    // int [][] != int[,] 気を付ける
    // int [][] は row の長さをバラバラにできてしまう
    int[,] map;
    GameObject[,] field;
    Vector2Int playerMoveVal;

    Vector2Int GetObjectIndex(string tag) {
        for (int y = 0; y < field.GetLength(0); ++y) {
            for (int x = 0; x < field.GetLength(1); ++x) {
                if (field[y, x] != null) {
                    if (field[y, x].tag.ToString() == tag) {
                        return new Vector2Int(x, y);
                    }
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(String tag, Vector2Int from, Vector2Int to) {
        if (to.x < 0 || to.x >= field.GetLength(1)) {
            return false;
        } else if (to.y < 0 || to.y >= field.GetLength(0)) {
            return false;
        }
        if (field[from.y, from.x].tag != tag) {
            return false;
        }

        if (field[to.y, to.x] != null) {
            if (field[to.y, to.x].tag == "Box") {
                if (!MoveNumber("Box", to, to + (to - from))) {
                    return false;
                }
            }
        }

        field[from.y, from.x].transform.position = new Vector3(to.x, field.GetLength(0) - to.y, 0.0f);

        field[to.y, to.x] = field[from.y, from.x];
        field[from.y, from.x] = null;
        return true;
    }
    // Start is called before the first frame update
    void Start() {
        map = new int[,] {
            { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        field = new GameObject[map.GetLength(0), map.GetLength(1)];
        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                if (map[y, x] == 1) {
                    field[y, x] = Instantiate(playerPrefab, new Vector3(x, field.GetLength(0) - y, 0.0f), Quaternion.identity);
                } else if (map[y, x] == 2) {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(x, field.GetLength(0) - y, 0.0f), Quaternion.identity);
                }
            }
        }

        playerMoveVal = new Vector2Int(0, 0);
    }

    // Update is called once per frame
    void Update() {
        playerMoveVal.x = Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow)) - Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow));
        playerMoveVal.y = Convert.ToInt32(Input.GetKeyDown(KeyCode.DownArrow)) - Convert.ToInt32(Input.GetKeyDown(KeyCode.UpArrow));
        if (playerMoveVal.x != 0 || playerMoveVal.y != 0) {
            Vector2Int playerIndex = GetObjectIndex("Player");
            MoveNumber("Player", playerIndex, playerIndex + playerMoveVal);
        }
    }
}
