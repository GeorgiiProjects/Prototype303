using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    // создаем переменную для силы взлета при нажатии space.
    public float floatForce;
    // создаем переменную для изменения гравитации, изменяем значения в инспекторе.
    public float gravityModifier;
    // создаем класс Rigidbody для доступа к Rigidbody Player.
    private Rigidbody playerRb;
    // создаем переменную для проверки соприкосновения с поверхностью.
    public bool isOnGround;
    // создаем переменную для проверки соприкосновения с бомбой (препятствием).
    public bool gameOver;
    // создаем класс ParticleSystem для того, чтобы использовать эффект взрыва у Player.
    public ParticleSystem explosionParticle;
    // создаем класс ParticleSystem для того, чтобы использовать эффект фейрверка у Player.
    public ParticleSystem fireworksParticle;
    // создаем класс AudioSource для того чтобы проигрывать звуковой эффект.
    private AudioSource playerAudio;
    // создаем класс AudioClip для того чтобы проигрывать звуковой эффект при подборе объекта деньги.
    public AudioClip moneySound;
    // создаем класс AudioClip для того чтобы проигрывать звуковой эффект при взрыве от объекта бомба.
    public AudioClip explodeSound;
    // создаем класс AudioClip для того чтобы проигрывать звуковой эффект при соприкосновении с Ground.
    public AudioClip blipSound;

    void Start()
    {
        // изменяем силу гравитации, gravityModifier например может быть равен 2.
        Physics.gravity *= gravityModifier;
        // получаем доступ к AudioSource player'a через компонент GetComponent.
        playerAudio = GetComponent<AudioSource>();
        // получаем доступ к Rigidbody player'a через компонент GetComponent, иначе нажатие на space не будет работать.
        playerRb = GetComponent<Rigidbody>();
        // происходит небольшой толчок мяча вверх.
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        // Если удерживаем кнопку пробел и игра не окончена.
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            // если позиция шарика по оси y < 16.
            if (transform.position.y < 16)
            {
                // взлет снова доступен.
                playerRb.AddForce(Vector3.up * floatForce);
            }          
        }
    }

    // создаем OnCollisionEnter для того чтобы определить когда Player соприкасается с землей и препятствиями так как на них есть boxcollider. 
    private void OnCollisionEnter(Collision other)
    {
        // если Player соприкасается с Bomb (тэг задан у объекта Bomb в инспекторе).
        if (other.gameObject.CompareTag("Bomb"))
        {
            // Запускаем ParticleSystem эффект взрыва при соприкосновении с Bomb, когда игрок умирает и игра заканчивается.
            explosionParticle.Play();
            // проигрываем звуковой эффект explodeSound с громкостью 1.0f, один раз, когда Player соприкасается с Bomb и умирает.
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            // игра заканчивается
            gameOver = true;
            // в логе пишется Game Over!
            Debug.Log("Game Over!");
            // Бомба взрывается, шар перестает двигаться.
            Destroy(other.gameObject);
        }

        // если Player соприкасается с Money (тэг задан у объекта Money в инспекторе)
        else if (other.gameObject.CompareTag("Money"))
        {
            // Запускаем ParticleSystem эффект фейрверка при соприкосновении с Money.
            fireworksParticle.Play();
            // проигрываем звуковой эффект moneySound с громкостью 1.0f, один раз, когда Player соприкасается с Money.
            playerAudio.PlayOneShot(moneySound, 1.0f);
            // Money уничтожаются, шар продолжает двигаться.
            Destroy(other.gameObject);

        }
        // если Player соприкасается с Ground (тэг задан у объекта Ground в инспекторе)
        if (other.gameObject.CompareTag("Ground"))
        {
            // Player соприкасается с поверхностью.
            isOnGround = true;
            // проигрываем звуковой эффект blipSound с громкостью 1.0f, один раз.
            playerAudio.PlayOneShot(blipSound, 1.0f);
        }
    }
}
