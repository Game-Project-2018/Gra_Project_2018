﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ViewRotationController : MonoBehaviour
{

    [Header("Freeze Rotation")]
    public bool x;
    public bool y;
    public bool z;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    void Start()
    {
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.x;
        zRotation = transform.eulerAngles.x;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 mousePos = ray.GetPoint(distance);
            transform.LookAt(mousePos + Vector3.up * transform.position.y);
        }
        FreezeRotation();
    }

    private void FreezeRotation()
    {
        if (x)
        {
            transform.eulerAngles = new Vector3(xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (y)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        }

        if (z)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
        }
    }
}