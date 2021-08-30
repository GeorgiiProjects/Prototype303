using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    // создаем массив GameObject[] objectPrefabs, в который поместим префабы бомбы и денег, в инспекторе.
    public GameObject[] objectPrefabs;
    // создаем переменную для задержки появления препятствия.
    private float spawnDelay = 2f;
    // создаем переменную для интервала появления препятствия.
    private float spawnInterval = 2f;
    // создаем класс PlayerControllerX для получения к нему доступа из SpawnManagerX скрипта.
    private PlayerControllerX playerControllerScript;

    void Start()
    {
        // создаем метод InvokeRepeating для того чтобы препятствия появлялись с одной и той же периодичностью
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        // GameObject.Find("Player") Ищем объект Player в иерархии unity и получаем доступ к скрипту PlayerControllerX через GetComponent<PlayerControllerX>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // создаем кастомный метод SpawnObjects, для того чтобы его можно было вызывать сколько понадобится раз.
    void SpawnObjects ()
    {
        // Используем Random.Range, чтобы объекты спавнились в разных точках от 5 до 15 по оси y.
        // Используем Vector3 spawnLocation для того, чтобы занести в формулу координаты спавна объектов.
        // Спавн объектов по осям x и z остаются неизменными, по оси x задано значение 30.
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        // создаем переменную index, в этом методе, так как только тут будем ее использовать. 
        // используем класс Random.Range для того, чтобы объекты спавнились со случайностью от 0 до длины массива объектов т.е. 2.
        int index = Random.Range(0, objectPrefabs.Length);

        // если в скрипте PlayerController игра продолжается
        if (!playerControllerScript.gameOver)
        {
            // Создаем копии objectPrefabs по номеру индекса массива, поворот объектов остается по умолчанию.
            // Используем spawnLocation в которой занесены кординаты спауна объектов по осям x,y,z.
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
