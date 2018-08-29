//=======================================================
// Author��     DuanBin
// Description��Create scripts to automatically add namespaces
//=======================================================
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class AutoAddNamespace : UnityEditor.AssetModificationProcessor
{

    private static readonly string AuthorCode =
      "//=======================================================\r\n"
    + "// Author:       DuanBin\r\n"   
    + "// CreateTime:   " + System.DateTime.Now.ToString() + "\r\n"
    + "// Description:  \r\n"
    + "//=======================================================\r\n";

    public static readonly string headCode =
    "using UnityEngine;\r\n"
    + "using System.Collections;\r\n"
    + "\r\n";

    public static void OnWillCreateAsset(string path)
    {
        //ֻ�޸�C#�ű�
        var scriptName = "";
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = "";
            allText += File.ReadAllText(path);
            scriptName = GetClassName(allText);
            if (scriptName != "")
            {
                CreateClass(path, scriptName);
            }
        }
    }

    //�����µ��� 
    public static void CreateClass(string path, string className)
    {
        var sb = new ScriptBuilder();
        //��������ռ�    UnoCfg.App.CodeName()->�Լ��������Ŀ�����ֶ�            
        sb.WriteLine("namespace" + " " + FirstLetterUppercase("JoyTheBall"));        
        sb.WriteCurlyBrackets();
        sb.Indent++;

        sb.WriteLine("public class #SCRIPTNAME# : MonoBehaviour");
        sb.WriteCurlyBrackets();
        sb.Indent++;

        var allText = AuthorCode + headCode + sb.ToString();
        //�滻�ű�����
        allText = allText.Replace("#SCRIPTNAME#", className);
        File.WriteAllText(path, allText);
    }

    //����ĸ�ĳɴ�д
    public static string FirstLetterUppercase(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        if (str.Length == 1)
            return str.ToUpper();

        var first = str[0];
        var rest = str.Substring(1);
        return first.ToString().ToUpper() + rest;
    }

    //��ȡunity�Զ������Ľű�����
    public static string GetClassName(string allText)
    {
        var patterm = "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour {\\s*\\/\\/ Use this for initialization\\s*void Start \\(\\) {\\s*}\\s*\\/\\/ Update is called once per frame\\s*void Update \\(\\) {\\s*}\\s*}";
        var match = Regex.Match(allText, patterm);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    }

}