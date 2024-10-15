using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance(){
        if(instance == null){
            GameObject obj = new GameObject();
            // 设置对象名称为脚本名
            obj.name = typeof(T).ToString();
            // 让这个单例过场景不移除，存在于脚本整个生命周期中
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}
