using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public float rotationSpeed = 50f; // скорость вращения в градусах в секунду

    void Update()
    {
        // Получаем текущий угол поворота объекта
        Vector3 currentRotation = transform.eulerAngles;

        // Вычисляем новый угол поворота, увеличивая каждую компоненту вектора на rotationSpeed за секунду
        Vector3 newRotation = currentRotation + new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f)) * Time.deltaTime;

        // Применяем новый угол поворота к объекту
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
