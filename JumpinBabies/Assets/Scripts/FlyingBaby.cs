using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBaby : MonoBehaviour
{
    [HideInInspector]
    public GameFlow gameFlowRef;

    bool dead = false;
    float maxHeight;
    float x, y;
    float xOffset = 0.2f;
    float speed = 0.01f;

    void Start()
    {
        maxHeight = this.transform.position.y + 3.48f;
        x = -1.75f;
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            float actPos = x + xOffset;

            x += speed;
            y = CalcYPos(((actPos + 20) % 1.0f)) * maxHeight;

            this.transform.position = new Vector3(x * 4, y - 3.48f, 0);
            this.transform.Rotate(Vector3.back * 5.0f * y, Space.Self);

            //check if someone catching me
            if (((actPos) - (actPos).round()).abs() < 0.01f)
            {
                if (actPos.roundInt() == 2)
                {
                    //baby saved
                    gameFlowRef.AddPointToPlayer();
                    gameFlowRef.flyingBabyCount -= 1;
                    Destroy(this.gameObject);
                }
                else if ((actPos).roundInt() + 1 != gameFlowRef.curPlayerPos)
                {
                    //baby dead
                    dead = true;
                    if (gameFlowRef.SoundOn)
                        this.GetComponent<AudioSource>().Play();
                    Destroy(this.gameObject, 2.0f);
                    this.GetComponent<Renderer>().enabled = false;
                    gameFlowRef.flyingBabyCount -= 1;
                    gameFlowRef.DecrementLives();
                }

            }
        }
    }

    float CalcYPos(float _x)
    {
        //_x += 0.1f;
        return -4 * _x.pow() + 4 * _x;
    }

    //[ExecuteInEditMode]
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    maxHeight = this.transform.position.y + 3.48f;

    //    for (float i = -1.75f; i < 2.0; i += 0.05f)
    //    {
    //        Gizmos.DrawSphere(new Vector3(i * 4, CalcYPos(((i + 0.2f + 20) % 1.0f)) * maxHeight - 3.48f, 0), 0.3f);
    //    }
    //}
}
