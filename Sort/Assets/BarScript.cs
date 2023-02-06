using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int Value = 1;
    //public MeshRenderer obj;

    void setValue(int i) {
        Value = i;
    }
    int getValue() {
        return Value;
    }
}
