using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_ : MonoBehaviour
{
    // Основные параметры
    public float speedMove; // Скорость передвижения
    public float jumpPower; // Сила прыжка
    // Параметры геимплея для персонажа
    private float gravityForce; // Гравитация персонажа
    [SerializeField]
    private Vector3 moveVector; // Направление движения персонажа
    // Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;
    private GameProcess GameProcess_;
    // Камера
    private GameObject Camera_Player;
    // Скорость преследования камерой игрока
    public float speedMoveCamera = 1f;

    public GameObject variableJoystickObj;
    public VariableJoystick variableJoystick;

    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        Camera_Player = GameObject.FindGameObjectWithTag("MainCamera");
        GameProcess_ = GetComponent<GameProcess>();
    }

    void FixedUpdate()
    {
        CharacterMove();
        GamingGravity();
    }
    void LateUpdate()
    {
        //Camera_Player.transform.position = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z - 8);

        //Vector3 PosCam = new Vector3(Camera_Player.transform.position.x, Camera_Player.transform.position.y + 8, Camera_Player.transform.position.z - 8);
        //Camera_Player.transform.position = Vector3.Lerp(PosCam, transform.position, Time.deltaTime * speedMoveCamera);
    }
    // Метод передвижения персонажа
    void CharacterMove()
    {
        if (!gameObject.GetComponent<GameProcess>().gamePause)
        {
            //Задание анимации ходьбы при нажатии клавиш передвижения
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("idle", false);
            else ch_animator.SetBool("idle", true);
            // перемещение по поверхности
            moveVector = Vector3.zero;


            if (GameProcess_.Joy)
            {
                variableJoystickObj.SetActive(true);
                moveVector.x = variableJoystick.Horizontal * (speedMove + GameProcess_.speedLvlUpg);
                moveVector.z = variableJoystick.Vertical * (speedMove + GameProcess_.speedLvlUpg);
            }
            else
            {
                variableJoystickObj.SetActive(false);
                moveVector.x = Input.GetAxis("Horizontal") * (speedMove + GameProcess_.speedLvlUpg);
                moveVector.z = Input.GetAxis("Vertical") * (speedMove + GameProcess_.speedLvlUpg);
            }



            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove + GetComponent<GameProcess>().speedLvlUpg / 100, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }

            moveVector.y = gravityForce;
            ch_controller.Move(moveVector * Time.deltaTime); // Метод передвижения по направлению
        }
        else
        {
            ch_animator.SetBool("idle", true);
            moveVector = Vector3.zero;
        }
    }
    // Метод гравитации
    void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
    }
}
