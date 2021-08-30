using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMake : MonoBehaviour
{
    public static bool ActiveBlock = false; // flag perehoda inisiotivi
    public static int weightmax = 11, heightmax = 19; // rezmeri polya
    public static Transform[,] grid = new Transform[weightmax, heightmax]; // mestopologenie ypavshih blokov
    public string[] AllClass;
    public interface IFigure
    {
        GameObject Fig { get; set; }
        void MoveRight();
        void MoveLeft();
        void MoveDown();
        void MoveSec();
        void RotateRight();
        void RotateLeft();
        bool MaxMove();
    }
   

    public class BaseFigure: IFigure
    {
        Vector3 rotationPoint;
        public GameObject Fig { get; set; }
        public void MoveRight()
        {
            Fig.transform.position += new Vector3(1, 0, 0);
            if (!MaxMove())
                Fig.transform.position += new Vector3(-1, 0, 0);
        }
        public void MoveLeft()
        {
            Fig.transform.position += new Vector3(-1, 0, 0);
            if (!MaxMove())
                Fig.transform.position += new Vector3(1, 0, 0);
        }
        public void MoveDown()
        {
            Fig.transform.position += new Vector3(0, -1, 0);
            if (!MaxMove())
                Fig.transform.position += new Vector3(0, 1, 0);
        }
        public void MoveSec()
        {
            Fig.transform.position += new Vector3(0, -1, 0);
            if (!MaxMove())
            {
                Fig.transform.position += new Vector3(0, 1, 0);
                ActiveBlock = false;
                AddGrid();
                CheckForLines();
                Fig.GetComponent<TetrisMake>().enabled = false;
            }
        }
        public void RotateRight()
        {
            Fig.transform.RotateAround(Fig.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!MaxMove())
            {
                Fig.transform.RotateAround(Fig.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        public void RotateLeft()
        {
            Fig.transform.RotateAround(Fig.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!MaxMove())
            {
                Fig.transform.RotateAround(Fig.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }
        void AddGrid()
        {
            foreach (Transform children in Fig.transform)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                grid[roundedX, roundedY] = children;

            }
        }
        public bool MaxMove()
        {
            foreach (Transform children in Fig.transform)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                if (roundedX < 0 || roundedX >= weightmax || roundedY < 0 || roundedY >= heightmax)
                {
                    return false;
                }
                if (grid[roundedX, roundedY] != null)
                    return false;
            }

            return true;
        }

        void CheckForLines()
        {
            for (int i = heightmax - 1; i >= 0; i--)
            {
                if (HasLine(i))
                {
                    DeleteLine(i);
                    RowDown(i);
                }
            }
        }

        bool HasLine(int i)
        {
            for (int j = 0; j < weightmax; j++)
            {
                if (grid[j, i] == null)
                    return false;
            }
            return true;
        }

        void DeleteLine(int i)
        {
            for (int j = 0; j < weightmax; j++)
            {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
            }
        }

        void RowDown(int i)
        {
            for (int y = i; y < heightmax; y++)
            {
                for (int j = 0; j < weightmax; j++)
                {
                    if (grid[j, y] != null)
                    {
                        grid[j, y - 1] = grid[j, y];
                        grid[j, y] = null;
                        grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    }
                }
            }
        }
    }
    public class t : BaseFigure 
    {
        
    }
    public class O : BaseFigure
    {

    }
    public class I : BaseFigure
    {

    }
    public class J : BaseFigure
    {

    }
    public class Z : BaseFigure
    {

    }
    public class E : BaseFigure
    {

    }
    public class L : BaseFigure
    {

    }

    public class FigureFactory
    {
        public T CreateFigure<T>() where T : IFigure
        {
            var types = typeof(T);
            System.Reflection.ConstructorInfo figa = types.GetConstructor(new Type[] { });
            object obj = figa.Invoke(new object[] { });
            //                                new Object[] { chars, 13, 10 });
            // var anyTypeName = typeof(T).Name;
            return (T)(object)obj;
        }
    }


    IFigure figura;

    private static float moveTime;
    private float speedTime = 0.8f;
    FigureFactory figureFactory = new FigureFactory();
    void Start()
    {
       
    }
    void Update()
    {

        //object obj = Activator.CreateInstance(typeof(String),
        //                                    Spawner.ActiveFigur.name.Insert(0, Spawner.ActiveFigur.name));/* { chars, 13, 10 });*/

        //figureFactory.CreateFigure<>();


        switch (Spawner.ActiveFigur.name)
        {
            case "T(Clone)":
                figura = figureFactory.CreateFigure<t>();
                //figura = new t();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "I(Clone)":
                figura = figureFactory.CreateFigure<I>();
                // figura = new I();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "O(Clone)":
                figura = figureFactory.CreateFigure<I>();
                //figura = new O();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "J(Clone)":
                figura = figureFactory.CreateFigure<I>();
                // figura = new J();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "L(Clone)":
                figura = figureFactory.CreateFigure<I>();
                // figura = new L();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "Z(Clone)":
                figura = figureFactory.CreateFigure<I>();
                //  figura = new Z();
                figura.Fig = Spawner.ActiveFigur;
                break;
            case "e(Clone)":
                figura = figureFactory.CreateFigure<I>();
                // figura = new E();
                figura.Fig = Spawner.ActiveFigur;
                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            figura.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            figura.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            figura.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            figura.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            figura.RotateRight();
        }
        if (Time.time - moveTime > speedTime)
        {
            figura.MoveSec();
            moveTime = Time.time;
        }
    }
}
