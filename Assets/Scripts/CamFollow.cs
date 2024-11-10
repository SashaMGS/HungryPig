using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;  // Ссылка на игрока
    public float smoothSpeed = 0.125f;  // Скорость плавного следования камеры
    public Vector3 offset;  // Смещение камеры относительно игрока

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;  // Желаемая позиция камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Плавное перемещение камеры к желаемой позиции
        transform.position = smoothedPosition;  // Применение новой позиции камеры
    }
}
