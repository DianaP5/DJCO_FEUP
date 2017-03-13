using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private PlayerMovement player;
    private GameObject destructionPoint;
    public string type;
    public float duration = 2f;
    public float factor = 1.5f;

    public GameObject powerupsound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        destructionPoint = GameObject.Find("PlatformDestroyPoint");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (type)
            {
                case "highJump":
                    player.StartCoroutine(HighJumpCountDown());
                    break;
                case "highSpeed":
                    player.StartCoroutine(HighSpeedCountDown(player, duration));
                    break;
                case "lowSpeed":
                    player.StartCoroutine(LowSpeedCountDown(player, duration));
                    break;
                case "magnet":
                    player.StartCoroutine(MagnetCountDown(player, duration));
                    break;
                default:
                    break;
            }

            powerupsound = GameObject.Find("Powerupsound");
            powerupsound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        if (transform.position.x < destructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator HighJumpCountDown()
    {
        player.jumpPower *= this.factor;
        yield return new WaitForSeconds(duration);
        player.jumpPower /= factor;
    }

    public IEnumerator HighSpeedCountDown(PlayerMovement player, float duration)
    {
        player.moveSpeed *= 2;
        yield return new WaitForSeconds(duration);
        player.moveSpeed /= 2;
    }

    public IEnumerator LowSpeedCountDown(PlayerMovement player, float duration)
    {
        player.moveSpeed /= 2;
        yield return new WaitForSeconds(duration);
        player.moveSpeed *= 2;
    }

    public IEnumerator MagnetCountDown(PlayerMovement player, float duration)
    {
        player.EnableMagnet();
        yield return new WaitForSeconds(duration);
        player.DisableMagnet();
    }
}
