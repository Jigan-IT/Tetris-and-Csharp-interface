using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static GameObject ActiveFigur;
    public GameObject[] Figurs;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (TetrisMake.ActiveBlock == false)
        {
            TetrisMake.ActiveBlock = true;
            ActiveFigur = Instantiate(Figurs[Random.Range(0, Figurs.Length)], new Vector3(5, 17, 0), Quaternion.identity);
        }
    }
}
