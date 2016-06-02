using UnityEngine;
using System.Collections;

public class MonsterBehaviour : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        animator = GetComponent<Animator>();
        animator.Play(ANIMATION_NAME.APEAR);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        transform.Translate(Vector3.left * GameManager.Instance.monsterConfiguration.Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        switch (collider.tag)
        {
            case OBJECT_TAG.RUNNER:
                animator.Play(ANIMATION_NAME.DIE);
                break;
        }
    }

    /// <summary>
    /// 该函数在Animation Editer的tmp_Dead最后一帧调用
    /// </summary>
    void Dead()
    {
        Singleton<RealtimeSymbolHostManager>.Instance.RemoveObject(gameObject);

        //销毁对象
        Destroy(gameObject);
    }

    //该函数在Animation Editer的tmp_Apear最后一帧调用
    void Run()
    {
        animator.Play(ANIMATION_NAME.RUN);
    }

}

