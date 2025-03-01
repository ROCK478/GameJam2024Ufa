using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using UnityEditor;

public class Player : MonoBehaviour // Класс игрока, вешается на главного героя
{
    [Header("Настройки передвижения")]
    [SerializeField] private float _moveSpeed;//Скорость передвижение
    private bool _lookRight = true; //Проверка, смотрит ли персонаж вправо
    private Rigidbody2D _rb; //Переменная для хранения физики
    private Animator _animator; // Переменная для хранения аниматора

    [Header("Настройки прыжка")]
    [SerializeField] private float _jumpForce; //Сила прыжка
    public bool _isGround; //Проверка нахождения на земле
    [SerializeField] private float _rayDistance = 0.6f; //Расстояние для поиска земли для недоступности множественных прыжков

    [Header("Настройки здоровья")]
    [SerializeField] private int _maxHealth = 4; //Максимальное здоровье
    public int _currentHealth; //Текущее здоровье

    [Header("Настройки тихой ходьбы")]
    [NonSerialized]public bool IsStealth = false; //Проверка, находится ли персонаж в состоянии Стелса
    [SerializeField] private float _stealthMoveSpeed;//Скорость передвижения в состоянии Стелса

    [Header("Настройки ближнего боя")]
    [SerializeField] private GameObject PlayerRangeForAttack; //Дальность атаки
    [SerializeField] private float _timeAttack; //Время для атаки
    [SerializeField] public int DamageMelee; //Урон

    private SpriteRenderer _sr; // Ссылка на Настройки Спрайта



    private void Awake() // До запуска игры
    {
        _sr = GetComponent<SpriteRenderer>(); // Получаем ссылку на Спрайт Рендер
        _animator = GetComponent<Animator>(); //Получаем ссылку на Аниматор
        _rb = GetComponent<Rigidbody2D>(); //Получаем ссылку на Физику
        PlayerRangeForAttack = transform.Find("PlayerRangeForAttack").gameObject; //Получаем ссылку на коллизию дальности атаки
    }
    private void Start() //При старте
    {
        _currentHealth = _maxHealth; //Присваиваем текущему здоровью максимальное
    }
    private void Update() // Каждый кадр
    {
        if (DialogManager.Stop == false) // Если диалог не проигрывается
        {
            Flip(); // Вызов функции смены стороны
            Jump(); // Вызов функции прыжка
            StealthMove(); //Вызов функции скрытной ходьбы
            MeleeAttack(); //Вызов функции атаки
        }
    }
    private void FixedUpdate() //Каждый кадр физики
    {
        if (DialogManager.Stop == false) // Если диалог не проигрывается
        {
            Move(); //Вызов функции ходьбы
        }
    }

    private void Move() //Ходьба
    {
        if (IsStealth)
        {
            _rb.velocity = (new Vector2(Input.GetAxis("Horizontal") * _stealthMoveSpeed, _rb.velocity.y)); // Ускорение по x при стелсе
        }
        else
        {
            _rb.velocity = (new Vector2(Input.GetAxis("Horizontal") * _moveSpeed, _rb.velocity.y)); // Обычное ускорение по x 
        }
        _animator.SetFloat("xVelocity", Math.Abs(_rb.velocity.x)); // Устанавливаем в аниматоре значение
    }

    private void Flip() //Поворот стороны
    {
        if ((_lookRight && (Input.GetAxis("Horizontal") < 0)) || (!_lookRight && (Input.GetAxis("Horizontal") > 0))) // Если пользователь решил поменять направление ходьбы
        {
            gameObject.transform.localScale *= new Vector2(-1, 1); // Меняем размер на -1
            _lookRight = !_lookRight; // Состояние меняется
        }
    }

    private void Jump() // Прыжок
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, _rayDistance, LayerMask.GetMask("Ground")); // Поиск земли под ногами, У объектов земли нужно указать слой "Ground"
        if (hit.collider != null) //Если Нашли землю
        {
            _isGround = true; //Состояние земли активно
        }
        else
        {
            _isGround = false; //Земля не найдена
            _animator.SetBool("isJump", _isGround); //Устанавливаем значение для аниматора
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGround) //Если нажат пробел и находимся на земле
        {
            _rb.AddForce(Vector2.up.normalized * _jumpForce, ForceMode2D.Impulse); // Добавляем силу для прыжка
            _animator.SetBool("isJump", _isGround); // Устанавливаем значение для аниматора
        }
    }

    public void TakeDamage(int damage) // Получение урона
    {
        _currentHealth -= damage; // Отнимаем от текущего здоровья полученный урон
        if (_currentHealth <= 0) // Если здоровье опустилось ниже нуля - загружаем уровень заново
        {
            SceneManager.LoadScene("Lvl 3");
        }
    }

    private void StealthMove() // Скрытная ходьба
    {
        if (Input.GetKey(KeyCode.LeftControl)) //Если нажат левый контрол
        {
            IsStealth = true; // Состояние Стелса активно
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && Input.GetAxis("Horizontal") != 0) //Если клавиша не нажата и персонаж пошёл
        {
            IsStealth = false; //Состояние стелса неактивно
        }
    }

    private void MeleeAttack() //Атака
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Если нажимаем клавишу мыши
        {
            _animator.SetTrigger("isAttack"); //Устанавливаем значение триггера
            PlayerRangeForAttack.SetActive(true); //Дистанция для атаки становится активна
            StartCoroutine(TimeDuration()); //Запускаем задержку после удара
        }
    }
    private IEnumerator TimeDuration() //Корутин для задержки
    {
        yield return new WaitForSeconds(0.2f); //Ждём 0.2 секунды
        PlayerRangeForAttack.SetActive(false); //Скрываем атаку
    }
}
