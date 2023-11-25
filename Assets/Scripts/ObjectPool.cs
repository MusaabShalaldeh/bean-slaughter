using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region singleton
    public static ObjectPool instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public enum ObjectTypes
    {
        bean = 0,
        floatingBean = 1,
        hitEffect = 2,
        bloodHitEffect = 3,
        coin = 4,
        blueBullets = 5,
        redBullets = 6,
    }

    [Header("Object Pools")]
    public List<GameObject> Beans;
    public List<GameObject> FloatingBeans;
    public List<GameObject> BamEffects;
    public List<GameObject> BloodEffects;
    public List<GameObject> Coins;
    public List<GameObject> BlueBullets;
    public List<GameObject> RedBullets;

    [Header("References")]
    public Transform DefaultPosition;

    public GameObject GetObject(ObjectTypes type, Vector3 pos)
    {
        GameObject obj;

        switch (type)
        {
            case ObjectTypes.bean:
                obj = PullObject(Beans, pos);
                break;
            case ObjectTypes.floatingBean:
                obj = PullObject(FloatingBeans, pos);
                break;
            case ObjectTypes.hitEffect:
                obj = PullObject(BamEffects, pos);
                break;
            case ObjectTypes.bloodHitEffect:
                obj = PullObject(BloodEffects, pos);
                break;
            case ObjectTypes.coin:
                obj = PullObject(Coins, pos);
                break;
            case ObjectTypes.blueBullets:
                obj = PullObject(BlueBullets, pos);
                break;
            case ObjectTypes.redBullets:
                obj = PullObject(RedBullets, pos);
                break;
            default:
                obj = PullObject(Coins, pos);
                break;
        }

        return obj;
    }

    public void ReturnObject(GameObject obj, ObjectTypes type, float waitTime = 0.0f)
    {
        switch (type)
        {
            case ObjectTypes.bean:
                StartCoroutine(ReturnObjectSequence(obj, Beans, waitTime));
                break;
            case ObjectTypes.floatingBean:
                StartCoroutine(ReturnObjectSequence(obj, FloatingBeans, waitTime));
                break;
            case ObjectTypes.hitEffect:
                StartCoroutine(ReturnObjectSequence(obj, BamEffects, waitTime));
                break;
            case ObjectTypes.bloodHitEffect:
                StartCoroutine(ReturnObjectSequence(obj, BloodEffects, waitTime));
                break;
            case ObjectTypes.coin:
                StartCoroutine(ReturnObjectSequence(obj, Coins, waitTime));
                break;
            case ObjectTypes.blueBullets:
                StartCoroutine(ReturnObjectSequence(obj, BlueBullets, waitTime));
                break;
            case ObjectTypes.redBullets:
                StartCoroutine(ReturnObjectSequence(obj, RedBullets, waitTime));
                break;
        }
    }

    GameObject PullObject(List<GameObject> list, Vector3 position)
    {
        if (list.Count < 1) return null;

        GameObject obj = list[0];
        list.Remove(obj);
        obj.transform.position = position;
        obj.SetActive(true);

        return obj;
    }

    IEnumerator ReturnObjectSequence(GameObject obj, List<GameObject> list, float waitTime = 0.0f)
    {
        yield return new WaitForSeconds(waitTime);

        obj.transform.position = DefaultPosition.position;
        list.Add(obj);
        obj.SetActive(false);
    }
}
