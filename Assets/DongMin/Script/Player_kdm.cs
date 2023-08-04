using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;




public class Player_kdm : MonoBehaviour
{
    public float Speed = 1f;
    public GameObject bullet;
    public GameObject HomingMissle;
    public GameObject helper;

    public Transform boompos = null;

    public Transform gunPos;
    public Transform gunPos2;
    public Transform gunPos3;
    public Transform SgunPos1;
    public Transform SgunPos2;
    public Transform SgunPos3;
    public Transform SgunPos4;
    public Transform SgunPos5;

    public GameObject boom = null;
    public GameObject lazer;
    public Image energybar;
    public Image energyStack;

    public int BoomStack = 2;

    public int AttackPower = 10;
    public int MaxItemCount = 4;
    public int MaxItem2Count = 4;
    public int ItemCount = 0;
    public int ItemCount2 = 0;
    public int Heart = 3;

    public float gazyStack;
    public float energyValue;
    public int stack = 0;

    bool volumeCheck = false;
    bool check = false;
    bool check2 = false;

    float limitX = 2.529f;
    float limitY = 4.695f;

    [SerializeField]
    bool dmScene = false;

    private void Start()
    {
        if (dmScene)
        {

            StartCoroutine(VolumeUp());
        }
        gazyStack = 0;
    }
    void Update()
    {
        Debug.Log("플레이어하트" + GameManagerSJ.Instance.player.Heart);
        Debug.Log("Bonus" + ScoreManager.instance.Bonus);

        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical, 0);

        if (dmScene)
        {
            transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -limitX, limitX),
            Mathf.Clamp(transform.position.y, -limitY, limitY));
        }


        if (Input.GetKey(KeyCode.Space) && !check)
        {
            if (!check2)
            {
                if (ItemCount2 == 1)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos.position, Quaternion.identity);
                }
                else if (ItemCount2 == 2)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos3.position, Quaternion.identity);
                }
                else if (ItemCount2 == 3)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos3.position, Quaternion.identity);
                }
                else if (ItemCount2 == 3)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, SgunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos3.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos4.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos5.position, Quaternion.identity);
                }
                else if (ItemCount2 == 4)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, SgunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos3.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos4.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos5.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos3.position, Quaternion.identity);
                }
            }


            if (ItemCount == 0)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
            }
            else if (ItemCount == 1)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos3.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
            }
            else if (ItemCount == 2)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, gunPos3.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
            }
            else if (ItemCount == 3)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, SgunPos2.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, SgunPos3.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, SgunPos4.position, Quaternion.identity);
                GameObject clone4 = Instantiate(bullet, SgunPos5.position, Quaternion.identity);

                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.25f, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone4.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.25f, 1));
            }
            else if (ItemCount == 4)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, SgunPos1.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, SgunPos2.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, SgunPos3.position, Quaternion.identity);
                GameObject clone4 = Instantiate(bullet, SgunPos4.position, Quaternion.identity);
                GameObject clone5 = Instantiate(bullet, SgunPos5.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.5f, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.25f, 1));
                clone4.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.25f, 1));
                clone5.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.5f, 1));
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && (gazyStack >= 100 && gazyStack < 300) && (stack >= 19 && stack < 47))
        {
            gazyStack -= 100;
            energybar.fillAmount = energybar.fillAmount - 0.19f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, gunPos.position, Quaternion.identity);

            // Instantiate(Lazer, pos.transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && (gazyStack >= 300 && gazyStack < 700) && (stack >= 47 && stack < 100))
        {
            gazyStack -= 300;
            energybar.fillAmount = energybar.fillAmount - 0.47f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, gunPos2.position, Quaternion.identity);
            Instantiate(helper, gunPos3.position, Quaternion.identity);
        }
        else if ((Input.GetKeyUp(KeyCode.Space) && gazyStack >= 700 && stack >= 100))
        {
            gazyStack = 0;
            energybar.fillAmount = 0f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, SgunPos3.position, Quaternion.identity);
            Instantiate(helper, SgunPos5.position, Quaternion.identity);

            Instantiate(lazer, SgunPos1.transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            stack = 0;
            energyStack.fillAmount = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Z) && BoomStack > 0)
        {
            BoomStack--;
            Instantiate(boom, boompos.transform.position, Quaternion.identity);
        }


        if (Heart < 1)
        {
            //Destroy(gameObject);
            Heart = 0;
        }

    }

    IEnumerator CreatBullet()  //쿨타임
    {
        check = true;
        yield return new WaitForSeconds(0.1f);
        check = false;
        energyStack.fillAmount += 0.02f;
        stack += 2;
    }

    IEnumerator CreatMissle()
    {
        check2 = true;
        yield return new WaitForSeconds(1f);
        check2 = false;
    }
    IEnumerator VolumeUp()
    {

        float scaleSpeed = 0.01f;
        yield return new WaitForSeconds(2.5f);

        while (gameObject.transform.localScale.x < 0.25f)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale = new Vector3(0.15f + scaleSpeed,
                0.15f + scaleSpeed, 0.15f + scaleSpeed);
            scaleSpeed += 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item1"))
        {
            ItemCount++;

            if (ItemCount >= MaxItemCount)
            {
                ItemCount = MaxItemCount;
                BoomStack++;
            }
        }

        if (collision.gameObject.CompareTag("Item2"))
        {
            ItemCount2++;

            if (ItemCount2 >= MaxItem2Count)
            {
                ItemCount2 = MaxItem2Count;
                BoomStack++;
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("InterCepter"))
        {
            //Heart -= 1;
        }
    }

    public void GazyPower(float energy)
    {


        gazyStack += energy;


        if (gazyStack <= 100)
        {
            //0.19가 1단계 풀스텍
            energybar.fillAmount += 0.0019f;

        }
        if (gazyStack > 100 && gazyStack <= 300)
        {
            //0.48가 2단계 풀스텍 // 0.28정도 채울 수 있음.
            energybar.fillAmount += 0.0014f;
            // powerStack = 1;

        }
        else if (gazyStack >= 300 && gazyStack < 700)
        {
            //1이 단계 풀스텍 // 0.52정도 채울 수 있음.
            energybar.fillAmount += 0.0013f;
            //  powerStack = 2;

        }
        else if (gazyStack == 700)
        {
            energybar.fillAmount = 1f;
            // powerStack = 3;
        }

        energyValue = energybar.fillAmount;
    }

}
