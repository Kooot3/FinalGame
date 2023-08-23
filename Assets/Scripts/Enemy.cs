using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2;
    private Animator animator;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        //видно прямо дублирующийся код, можно оптимизировать

        // GameObject root = new GameObject(name + "_Root");
        // root.transform.position = transform.position;
        // transform.SetParent(root.transform);
        // GameObject waypoints = new GameObject("Waypoints");
        // waypoints.transform.SetParent(root.transform);
        // waypoints.transform.position = root.transform.position;
        // GameObject p1 = new GameObject("Point1");
        // p1.transform.SetParent(waypoints.transform);
        // p1.transform.position = root.transform.position;
        // GameObject p2 = new GameObject("Point2");
        // p2.transform.SetParent(waypoints.transform);
        // p2.transform.position = root.transform.position;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        Transform CreateWayPoint(string title)
        {
            var p = new GameObject(title);
            p.transform.SetParent(waypoints.transform);
            p.transform.position = root.transform.position;
            return p.transform;
        }

        points = new List<Transform>();
        points.Add(CreateWayPoint("Point1"));
        points.Add(CreateWayPoint("Point2"));
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];

        transform.localScale = new Vector3(
            goalPoint.transform.position.x > transform.position.x
                ? -1
                : 1,
            1,
            1
        );
        // if (goalPoint.transform.position.x > transform.position.x)
        // {
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }
        // else
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        // }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)//хардкод
        {
            if (nextID == points.Count - 1)
            {
                idChangeValue = -1;
            }

            if (nextID == 0)
            {
                idChangeValue = 1;
            }

            nextID += idChangeValue;
        }
    }

    //Объект уничтожает игрока при столкновении,тем самым перезапускает сцену 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }

    //Чтобы задестроить противника при его убийстве
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
//public float speed;
//public Vector3[] positions;
//private int _currentTarget;

//private void Flip()
//{
//   Vector3 scale = transform.localScale;
//    scale.x *= -1f;
//    transform.localScale = scale;
//}

//private void FixedUpdate()
//{
//   transform.position = Vector3.MoveTowards(transform.position, positions[_currentTarget], speed);
//    if (transform.position == positions[_currentTarget])
//    {
//        if (_currentTarget < positions.Length - 1)
//        {
//            _currentTarget++;
//        }
//        else
//        {
//            _currentTarget = 0;
//        }
//        {
//            Flip();
//        }
//    }
//}