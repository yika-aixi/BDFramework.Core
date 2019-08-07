﻿using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using BDFramework.Editor.Tools;
using BDFramework.Helper;

public class EditorWindow_GenAssetBundle : EditorWindow
{
    /// <summary>
    /// 资源下面根节点
    /// </summary>
    public string rootResourceDir = "Resource/Runtime/";
    private bool isSelectIOS      = false;
    private bool isSelectAndroid  = true;
    //
    void DrawToolsBar()
    {
        GUILayout.Label("平台选择:");
        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(30);
            isSelectAndroid = GUILayout.Toggle(isSelectAndroid, "生成Android资源(Windows公用)");
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(30);
            isSelectIOS = GUILayout.Toggle(isSelectIOS, "生成iOS资源");
        }
        GUILayout.EndHorizontal();
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical();
        TipsGUI();
        DrawToolsBar();
        GUILayout.Space(10);
        LastestGUI();
        GUILayout.EndVertical();
    }

    /// <summary>
    /// 提示UI
    /// </summary>
    void TipsGUI()
    {
        GUILayout.Label("2.资源打包",EditorGUIHelper.TitleStyle);
        GUILayout.Space(5);
        GUILayout.Label(string.Format("资源根目录:Assets/{0}", rootResourceDir));
        GUILayout.Label(string.Format("AB输出目录:{0}",exportPath ));

        options = (BuildAssetBundleOptions) EditorGUILayout.EnumPopup("压缩格式:", options);
    }

    private BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;
    private string exportPath = "";
    /// <summary>
    /// 最新包
    /// </summary>
    void LastestGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("检测资源", GUILayout.Width(100)))
        {
            exportPath = EditorUtility.OpenFolderPanel("选择导出目录", Application.dataPath,"");


            if (string.IsNullOrEmpty(exportPath))
            {
                return;
            }
            
            if (isSelectAndroid)
                AssetBundleEditorTools.CheackAssets(rootResourceDir,exportPath+"/Android", BuildTarget.Android);
            if (isSelectIOS)
                AssetBundleEditorTools.CheackAssets(rootResourceDir,exportPath+"/iOS", BuildTarget.iOS);
            
            AssetDatabase.Refresh();
            Debug.Log("资源打包完毕"); 
        }

        if (GUILayout.Button("收集Shader keyword", GUILayout.Width(200)))
        {
            ShaderCollection.GenShaderVariant();
        }

        if (GUILayout.Button("一键打包[美术资源]", GUILayout.Width(380), GUILayout.Height(30)))
        { 
            exportPath = EditorUtility.OpenFolderPanel("选择导出目录", Application.dataPath,"");
            if (string.IsNullOrEmpty(exportPath))
            {
                return;
            }
            //开始打包
            BuildAsset();
        }

        GUILayout.EndVertical();
    }


    public void BuildAsset()
    {
        if (isSelectAndroid)
            AssetBundleEditorTools.GenAssetBundle(rootResourceDir,exportPath+"/"+ BDUtils.GetPlatformPath(RuntimePlatform.Android), BuildTarget.Android,options);
        if (isSelectIOS)
            AssetBundleEditorTools.GenAssetBundle(rootResourceDir,exportPath+"/"+BDUtils.GetPlatformPath(RuntimePlatform.IPhonePlayer), BuildTarget.iOS,options);
            
        AssetDatabase.Refresh();
        Debug.Log("资源打包完毕");
    }
}