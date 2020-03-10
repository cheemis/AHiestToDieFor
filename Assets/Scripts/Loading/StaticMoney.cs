using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMoney : MonoBehaviour
{

    private static float moneyCount = 0;

    private static int robbersAlive = 0;

    private static int lastScene = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetMoneyCount() {return moneyCount;}
    public static int GetRobbersAlive() {return robbersAlive;}

    public static void AddMoney(float money) {moneyCount += money;}
    public static void SetRobbersAlive(int robbers) {robbersAlive = robbers;}

    public static int GetLastScene() {return lastScene;}
    public static void SetLastScene(int scene) {lastScene = scene;}
}
