/*
* ==============================
* FileName:		BallTest
* Author:		DuanBin
* CreateTime:	8/28/2018 3:52:18 PM
* Description:		
* ==============================
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour {

    SphereCollider sphereCollider;

    public float bounceSpeed;
    public float gravity;
    public float maxSpeed = 1;

    float radius = 0;
    float speed;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        radius = sphereCollider.radius;

    }
    //������ƶ�����
    void Drop()
    {
        //ÿ֡��ȥģ�������������ٶ�Ӱ��
        speed -= gravity * Time.deltaTime;
        //�������ܴﵽ������ٶ�
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        transform.position += new Vector3(0, speed, 0);
    }
    void Bounce()
    {
        //���򷴵�������ʱ���������
        if (speed >= 0)
        {
            return;
        }

        //ȷ��һ��λ�ú��ʵ������壬������С��λ��ƫ��
        Vector3 p = transform.position + new Vector3(0, -radius, 0);
        Vector3 size = new Vector3(radius * 0.5f, radius * 0.5f, radius * 0.5f);
        if (Physics.OverlapBox(p, size, Quaternion.identity, LayerMask.GetMask("Ground")).Length > 0)
        {
            speed = bounceSpeed;
        }

    }

    void Update()
    {
        Bounce();
        Drop();

    }
}
