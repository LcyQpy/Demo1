using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleTest : MonoBehaviour
{
    public GameObject img;
    void Start()
    {
        // // 加载AB包 AB包不能重复加载
        // AssetBundle abasset = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        // // 加载资源
        // GameObject obj1 = abasset.LoadAsset<GameObject>("TestAsset");
        // GameObject obj2 = abasset.LoadAsset("TestCube", typeof(GameObject)) as GameObject;
        // obj1.GetComponent<Transform>().position = new Vector2(20 , 20);
        // Instantiate(obj1);
        // Instantiate(obj2);
        // StartCoroutine(LoadABasset("img", "test"));
        // AssetBundle.UnloadAllAssetBundles(false);
        // // 利用主包获取依赖信息
        // // 加载主包
        // AssetBundle mainAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        // // 加载主包中的固定信息
        // AssetBundleManifest assetBundleManifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        // // 从固定信息中得到依赖信息
        // string[] str = assetBundleManifest.GetAllDependencies("model"); // 从主包拿到依赖信息后根据要加载的包名来获取该包的依赖包
        // foreach(var i in str){
        //     Debug.Log(i);
        //     AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + i);
        // }
        
        // 加载AB包
        // AssetBundle imgAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "img");    // 全路径名
        // img.GetComponent<Image>().sprite = imgAB.LoadAsset<Sprite>("test");

        // 使用ABMgr加载需要的资源
        Sprite sp1= ABManager.GetInstance().LoadRes<Sprite>("img", "test");
        img.GetComponent<Image>().sprite = sp1;
    }
    // Update is called once per frame
    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.Space)){
            //AssetBundle.UnloadAllAssetBundles(true);
            ABManager.GetInstance().ClearAB(true);
            //ABManager.GetInstance().UnLoad("model", true);
        }
    }
}
