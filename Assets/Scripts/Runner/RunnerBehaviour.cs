using UnityEngine;
using System.Collections;

public class RunnerBehaviour : MonoBehaviour
{
    #region

    [Tooltip("Runner对象")]
    public GameObject Runner;

    #endregion

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        //建立全局的引用，方便其他地方访问
        //GameManager.Runner = transform;
        Singleton<RunnerManager>.Instance.Runner = this.gameObject;
        animator = GetComponent<Animator>();

        animator.Play(ANIMATION_NAME.RUN);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case OBJECT_TAG.MONSTER:
            case OBJECT_TAG.OBSTACLE:
                animator.Play(ANIMATION_NAME.DIE);
                GameManager.Instance.GameOver();
                break;
        }
    }

//     void OnCollisionEntered2D(Collider2D collider)
//     {
//         Debug.Log("OnCollisionEntered2D fuck you Monster!");
//    }
    void Dead()
    {
        Destroy(gameObject);
    }
}
