using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
    
public class Game : MonoBehaviour
{
    // Singleton class
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new List<Route>();

    private int totalRoute;
    private int successfulParks;

    //Events:
    public UnityAction<Route> OnCarEntersRoute;
    public UnityAction OnCarCollision;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoute = transform.GetComponentsInChildren<Route>().Length;
        successfulParks = 0;

        UnlockNewLevel();
        OnCarEntersRoute += OnCarEntersRouteHandler;
        OnCarCollision += OnCarCollisionHandler;
        
    }

    private void OnCarCollisionHandler()
    {
        Debug.Log("GameOver");
        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel);
        });

    }

    private void OnCarEntersRouteHandler(Route route)
    {
        route.car.StopDancingAnim();
        successfulParks++;

        if(successfulParks == totalRoute)
        {
            Debug.Log("You Win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                else
                {
                    Debug.Log("No More Levels to Load");
                }
            });
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex <= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        if(readyRoutes.Count == totalRoute)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach(var route in readyRoutes)
        {
            route.car.Move(route.linePoints);
        }
    }
}
