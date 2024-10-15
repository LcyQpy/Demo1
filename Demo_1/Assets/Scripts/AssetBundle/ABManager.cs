using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 1. AB 相关Api
/// 2. 单例
/// 3. 委托——>Lambda表达式
/// 4. 协程
/// 5. 字典
/// 让外部更方便进行资源管理
/// </summary>
public class ABManager : SingletonMono<ABManager>
{
    // 记录主包
    private AssetBundle MainAB = null;
    // 记录主包固定信息
    private AssetBundleManifest manifest = null;
    // 不能重复加载，存储加载过的包
    private Dictionary<string, AssetBundle> abDic= new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 包的存放路径 方便修改
    /// </summary>
    private string PathUrl{
        get{
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// 主包路径
    /// </summary>
    private string MianABName{
        get{
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else 
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName){
        AssetBundle ab = null;
        // 加载AB包
        if(MainAB == null){
            MainAB = AssetBundle.LoadFromFile(PathUrl + MianABName);
            // 获取依赖包
            manifest = MainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        // 然后获取依赖文件
        string[] str = manifest.GetAllDependencies(abName);
        // 加载依赖包
        foreach(var pName in str){
            if(!abDic.ContainsKey(pName)){
                ab = AssetBundle.LoadFromFile(PathUrl + pName);
                abDic.Add(pName, ab);
            }
        }
        // 加载目标资源包
        if(!abDic.ContainsKey(abName)){
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
    
    // 同步加载 不指定类型
    public Object LoadRes(string abName, string resName){
        // 加载资源
        LoadAB(abName);
        return abDic[abName].LoadAsset(resName);
    }

    // 同步加载 根据类型加载资源
    public Object LoadRes(string abName, string resName, System.Type type){
        // 加载资源
        LoadAB(abName);
        return abDic[abName].LoadAsset(resName, type); // 针对同名不同类型的资源
    }

    // 同步加载 泛型指定类型
    public T LoadRes<T>(string abName, string resName)where T : Object{
        // 加载资源
        LoadAB(abName);
        return abDic[abName].LoadAsset<T>(resName); // 针对同名不同类型的资源
    }

    // 异步 加载资源
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack){
        StartCoroutine(ReallLoadResAnsyc(abName, resName, callBack));
    }
    private IEnumerator ReallLoadResAnsyc(string abName, string resName, UnityAction<Object> callBack){
        LoadAB(abName);
        AssetBundleRequest abs =  abDic[resName].LoadAssetAsync(resName);
        yield return abs;
        callBack(abs.asset);
    }
    // 异步 加载资源 根据Type
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack, System.Type type){
        StartCoroutine(ReallLoadResAnsyc(abName, resName, callBack, type));
    }
    private IEnumerator ReallLoadResAnsyc(string abName, string resName, UnityAction<Object> callBack, System.Type type){
        LoadAB(abName);
        AssetBundleRequest abs =  abDic[resName].LoadAssetAsync(resName, type);
        yield return abs;
        callBack(abs.asset);
    }
    // 异步加载 根据泛型
    public void LoadResAsync<T>(string abName, string resName, UnityAction<Object> callBack){
        StartCoroutine(ReallLoadResAnsyc<T>(abName, resName, callBack));
    }
    private IEnumerator ReallLoadResAnsyc<T>(string abName, string resName, UnityAction<Object> callBack){
        LoadAB(abName);
        AssetBundleRequest abs =  abDic[resName].LoadAssetAsync<T>(resName);
        yield return abs;
        callBack(abs.asset);
    }
    // 单个包卸载
    public void UnLoad(string abName, bool end = false){
        if(abDic.ContainsKey(abName)){
            // 卸载包不卸载资源
            abDic[abName].Unload(end);
            abDic.Remove(abName);
        }
    }
    // 所有包卸载
    public void ClearAB(bool end = false){
        AssetBundle.UnloadAllAssetBundles(end);
        abDic.Clear();
        MainAB = null;
        manifest = null;
    }

}
