//using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Array : MonoBehaviour
{

    //game objects
    public GameObject Bar;
    public AudioSource Beep;
    public Text Status;
    float Time = 0.25f;

    //[SerializeField]
    //public Renderer BarC;
    //public Color newColor;


    public BarScript script;

    //array objects
    private GameObject[] graph = new GameObject[100];
    private Vector3 tempPos;

    //dropdown options
    bool QS = true;
    bool MS = false;
    bool BS = false;

    void Start(){
        //script = GameObject.FindGameObjectWithTag("logic").GetComponent<BarScript>();
        //float j = 0;
        for (int i = 0; i < 100; i++)
        {
            graph[i] = Instantiate(Bar, transform.position, transform.rotation);
            graph[i].transform.localScale = new Vector3(1, (1 + i) / 2f, 1);
            graph[i].transform.position = new Vector3(-50 + i, 1, 1);
            //OBJ.material.color = Color.blue;
            //newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            //BarC.material.SetColor("_Color", newColor);
            setValue(graph[i], i + 1);
            

        }
        //Debug.Log(graph[2].GetComponent<BarScript>().Value);
        Debug.Log(Random.Range(0, 100));
    }

    public void StartSorting()
    {
        //START = true;
        if (QS)
        {
            Status.text = "Sorting Algorithum: QuickSort\nStatus: Sorting...";
            StartCoroutine(quickSortIterative(graph, 0, 100-1));
        }
        if (MS)
        {
            Status.text = "Sorting Algorithum: MergeSort\nStatus: Sorting...";
            StartCoroutine(mergeSort(graph, 100));
        }
        if (BS)
        {
            Status.text = "Sorting Algorithum: BubbleSort\nStatus: Sorting...";
            StartCoroutine(TimeBS());
        }

    }

    public void end() {
        Application.Quit();
    }

    public void Shuffle()
    {
        Status.text = "Sorting Algorithum: Shuffling...\nStatus: Shuffling...";
        StartCoroutine(TimeShuffle());
    }

    IEnumerator TimeShuffle()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);

            int RandomIndex = Random.Range(0, 100);
            swap(graph, graph[i], graph[RandomIndex], i, RandomIndex);
        }
        Beep.Play();
        Status.text = "<Awaiting To Start>\nStatus: Completed";
    }


    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            Debug.Log("QuickSort has been selected");
            QS = true;
            MS = false;
            BS = false;
        }
        if (val == 1)
        {
            Debug.Log("MergeSort has been selected");
            QS = false;
            MS = true;
            BS = false;
        }
        if (val == 2)
        {
            Debug.Log("BubbleSort has been selected");
            QS = false;
            MS = false;
            BS = true;
        }
    }

    void setValue(GameObject i, int value)
    {
        i.GetComponent<BarScript>().Value = value;
    }

    int GetValue(GameObject i)
    {
        return i.GetComponent<BarScript>().Value;

    }

    int partition(GameObject[] arr,int low, int high){
        //int temp;
        int pivot = GetValue(arr[high]);

        int i = (low - 1);
        for (int j = low; j <= high - 1; j++){

            if (GetValue(arr[j]) <= pivot)
            {
                i++;
                //swap
                swap(arr, arr[i], arr[j], i, j);
            }
        }

        swap(arr, arr[i + 1], arr[high], i + 1, high);

        return i + 1;
    }


    // got some help from GFG for converting quick sort to non recursive
    //https://www.geeksforgeeks.org/iterative-quick-sort/
    IEnumerator quickSortIterative(GameObject[] arr, int l, int h){
        int[] stack = new int[h - l + 1];

        int top = -1;

        stack[++top] = l;
        stack[++top] = h;

        while (top >= 0)
        {
            yield return new WaitForSeconds(Time);
            h = stack[top--];
            l = stack[top--];

            int p = partition(arr, l, h);
            Beep.Play();

            if (p - 1 > l)
            {
                stack[++top] = l;
                stack[++top] = p - 1;
            }

            if (p + 1 < h)
            {
                stack[++top] = p + 1;
                stack[++top] = h;
            }
        }
        Status.text = "Sorting Algorithum: QuickSort\nStatus: Completed";
    }


    //https://www.geeksforgeeks.org/iterative-merge-sort/
    IEnumerator mergeSort(GameObject[] arr, int n)
    {

        int curr_size;

        int left_start;

        for (curr_size = 1; curr_size <= n - 1;curr_size = 2 * curr_size)
        {

            for (left_start = 0; left_start < n - 1;left_start += 2 * curr_size)
            {
                //possible time delay here
                int mid = System.Math.Min(left_start + curr_size - 1, n - 1);

                int right_end = System.Math.Min(left_start + 2 * curr_size - 1, n - 1);
                yield return new WaitForSeconds(Time);
                //StartCoroutine(TimeMS(arr, left_start, mid, right_end));// this get converted into a time delay function
                TimeMS(arr, left_start, mid, right_end);
                Beep.Play();
            }
        }
        Status.text = "Sorting Algorithum: MergeSort\nStatus: Completed";
    }

    void TimeMS(GameObject[] arr, int l, int m, int r)
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;

        GameObject[] L = new GameObject[n1];
        GameObject[] R = new GameObject[n2];

        for (i = 0; i < n1; i++)
            L[i] = arr[l + i];
        for (j = 0; j < n2; j++)
            R[j] = arr[m + 1 + j];

        i = 0;
        j = 0;
        k = l;
        while (i < n1 && j < n2)
        {
            //yield return new WaitForSeconds(0.25f);
            if (GetValue(L[i]) <= GetValue(R[j]))
            {
                sortingSwapOverwrite(k, L[i]);
                arr[k] = L[i];
                i++;
            }
            else
            {
                sortingSwapOverwrite(k, R[j]);
                arr[k] = R[j];
                j++;
            }
            k++;
        }
        while (i < n1)
        {
            //yield return new WaitForSeconds(0.25f);
            sortingSwapOverwrite(k, L[i]);
            arr[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            //yield return new WaitForSeconds(0.25f);
            sortingSwapOverwrite(k, R[j]);
            arr[k] = R[j];
            j++;
            k++;
        }
    }




    void sortingSwapOverwrite(int k, GameObject LR)
    {
        LR.transform.position = new Vector3(-50 + k, 1, 1);
    }

    [ContextMenu("Print Array")]
    void printarray()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(GetValue(graph[i]));
        }
    }

    [ContextMenu("Print Array pos")]
    void printarraypos()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(graph[i].transform.position);
        }
    }

    IEnumerator TimeBS()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(Time);
            for (int j = 0; j < 100; j++)
            {
                if (GetValue(graph[i]) < GetValue(graph[j]))
                {
                    swap(graph, graph[i], graph[j], i, j);
                }
            }
            Beep.Play();
        }
        //START = false;
        Status.text = "Sorting Algorithum: BubbleSort\nStatus: Completed";

    }

    void swap(GameObject[] a, GameObject i, GameObject j, int indexi, int indexj)
    {
        //Time.deltaTime;
        tempPos = i.transform.position;
        i.transform.position = j.transform.position;
        j.transform.position = tempPos;

        a[indexi] = j;
        a[indexj] = i;
    }

}