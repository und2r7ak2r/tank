using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followtarget : MonoBehaviour
{
    public Transform tank1;
    public Transform tank2;
    private Vector3 offset;  //摄像机与两个坦克中心点的偏移，保持视角始终在两个坦克的中间从而看到两个坦克
    public Camera camera;
    void Start()
    {
        offset=transform.position-(tank1.position+tank2.position)/2;
        camera.GetComponent<Camera>();
    }

    
    void Update()
    {
        if(tank1==null||tank2==null) return;
        transform.position=offset+(tank1.position+tank2.position)/2;
        float distance=Vector3.Distance(tank1.position,tank2.position); 
        float size = distance * 0.83f; // 0.83f是一个经验值，可以根据实际情况调整，以确保两个坦克都在视野范围内
        camera.orthographicSize = size; // 调整摄像机的正交大小以适应两个坦克之间的距离
    }
}
