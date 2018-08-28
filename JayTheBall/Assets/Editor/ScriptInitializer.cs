/*
* ==============================
* FileName:		ScriptInitializer
* Author:		DuanBin
* CreateTime:	8/6/2018 4:36:16 PM
* Description:	用于初始化脚本文件，使用前请先修改Unity的脚本模板，该文件放置于Editor文件夹下	
* ==============================
*/
using System.IO;

public class ScriptInitializer : UnityEditor.AssetModificationProcessor {

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.ToLower().EndsWith(".cs") || path.ToLower().EndsWith(".lua"))
        {
            string content = File.ReadAllText(path);

            content = content.Replace("#AUTHORNAME#", "DuanBin");
            content = content.Replace("#CREATETIME#", System.DateTime.Now.ToString());

            File.WriteAllText(path, content);
        }
    }    
}


