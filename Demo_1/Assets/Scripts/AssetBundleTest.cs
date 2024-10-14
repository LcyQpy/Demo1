using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleTest : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        // 加载AB包 AB包不能重复加载
        AssetBundle abasset = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        // 加载资源
        GameObject obj1 = abasset.LoadAsset<GameObject>("TestAsset");
        GameObject obj2 = abasset.LoadAsset("TestCube", typeof(GameObject)) as GameObject;
        obj1.GetComponent<Transform>().position = new Vector2(20 , 20);
        Instantiate(obj1);
        Instantiate(obj2);
        StartCoroutine(LoadABasset("img", "test"));
    }
    IEnumerator LoadABasset(string ABname, string Assetname){
        // 加载AB包
        AssetBundleCreateRequest asset = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABname);
        yield return asset;
        // 加载资源
        AssetBundleRequest sp1 = asset.assetBundle.LoadAssetAsync<Sprite>(Assetname);
        yield return sp1;
        img.sprite = sp1.asset as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
