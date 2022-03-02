using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent: window
public class BabyThrower : MonoBehaviour
{
    [HideInInspector]
    public GameFlow gameFlowRef;

    GameObject BabyPrefab;

    void Start()
    {
        BabyPrefab = Resources.Load<GameObject>("Baby");
        StartThrowing();
    }

    void StartThrowing()
    {
        StartCoroutine(ThrowingProcess());
    }

    IEnumerator ThrowingProcess()
    {
        while (true)
        {

            float _timeToWait = Random.Range(2, 4) + 0.5f;//Random.Range(2.3f, 3.7f);
            yield return new WaitForSeconds(_timeToWait);

            if (gameFlowRef.flyingBabyCount < 5)
            {
                gameFlowRef.flyingBabyCount += 1;
                Instantiate(BabyPrefab,/*window*/this.transform.position, Quaternion.identity, null).
                    GetComponent<FlyingBaby>().gameFlowRef = gameFlowRef;
            }
        }
    }
}
