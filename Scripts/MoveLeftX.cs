using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    // создаем переменную для скорости перемещения влево бэкграунда и препятствий.
    public float speed;
    // создаем класс PlayerControllerX для получения к нему доступа из MoveLeftX скрипта.
    private PlayerControllerX playerControllerScript;
    // создаем переменную обозначающую минусовую границу уничтожения препятствий по оси x, т.е. -x.
    private float leftBound = -10;

    void Start()
    {
        // GameObject.Find("Player") Ищем объект Player в иерархии unity и получаем доступ к скрипту PlayerControllerX через GetComponent<PlayerController>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    void Update()
    {
        // если в скрипте PlayerController игра продолжается.
        if (!playerControllerScript.gameOver)
        {
            // используем transform.Translate для перемещения объектов влево.
            // используем Vector3.left * speed для того чтобы указать направление влево и скорость передвижения объектов.
            // используем Time.deltaTime для плавности перемещения объекта за 1 секунду на равные расстояния, на любом пк.
            // Space.World координаты изменяются в глобальном пространстве.
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // если объект выходит за пределы по оси -х и не содержит тэг "Background"
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            // уничтожаем его.
            Destroy(gameObject);
        }
    }
}
