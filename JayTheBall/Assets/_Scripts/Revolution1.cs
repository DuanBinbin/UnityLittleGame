/*
* ==============================
* FileName:		Revolution1
* Author:		DuanBin
* CreateTime:	8/29/2018 11:40:02 AM
* Description:		
* ==============================
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution1 : MonoBehaviour {

    public GameObject sun;

    [Header("半径")]
    public float Radius; //半径
    public float Angle; //角度
    public float speed = 10;

    public float x;
    public float y;
    public float z;

    void Awake()
    {
        transform.position = new Vector3(10 * Random.value, 10 * Random.value, 0); //重置做圆周的开始位置

        //GameObject sun = GameObject.FindGameObjectWithTag("sun"); //取得圆点 我用一个sphere 表示
        Radius = Vector3.Distance(transform.position, sun.transform.position); //两个物品间的距离
        Angle = 0.3f; // ---角速度
        speed = 1 * Random.value; // 这个应该所角速度了
    }
    

    void Update()
    {
        //下面的概念有点模糊了
        Angle += speed * Time.deltaTime; // 

        x = Mathf.Cos(Angle) * Radius;
        y = Mathf.Sin(Angle) * Radius;
        z = Mathf.Sin(Angle) * Radius;


        transform.position = new Vector3(x, 0, z);
    }

}
