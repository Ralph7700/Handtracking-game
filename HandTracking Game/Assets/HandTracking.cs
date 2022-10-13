using System.Collections.Generic;
using UnityEngine;
 
public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] handPoints;
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
 
        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        print(data);
        string[] points = data.Split(',');
        print(points[0]);
 
        //0        1*3      2*3
        //x1,y1,z1,x2,y2,z2,x3,y3,z3
 
        for ( int i = 0; i<21; i++)
        {
 
            float x = 7-float.Parse(points[i * 3])/75;
            float y = float.Parse(points[i * 3 + 1]) / 75;
            float z = float.Parse(points[i * 3 + 2]) / 75;
 
            handPoints[i].transform.position =  new Vector3(Interpolate(handPoints[i].transform.position.x,-x,0.05f),
                                                            Interpolate(handPoints[i].transform.position.y,y,0.05f),
                                                            Interpolate(handPoints[i].transform.position.z,z,0.05f));
            
        }

 
    }
    private float Interpolate(float a , float b , float f){
        return (a * (1.0f - f)) + (b * f);
    }
}
