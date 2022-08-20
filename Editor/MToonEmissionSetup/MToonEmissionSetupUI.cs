using Jirno.MToonEmissionSetup.MToonEmissionSetup;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jirko.Unity.VRoidAvatarUtils.MToonEmissionSetup
{
    /// <summary>
    /// マテリアルの設定をコピーするやつの GUI
    /// </summary>
    public class MToonEmissionSetupUI : EditorWindow
    {

        /// <summary>
        /// 対象GameObject
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// コピー元EmissionTexture
        /// </summary>
        public Texture SourceTexture;

        /// <summary>
        /// 添え字のプロパティをセットするか？
        /// </summary>
        public bool[] IsSetProperty = new bool[0];

        /// <summary>
        /// マテリアルの数
        /// </summary>
        private int materialCount = 0;

        /// <summary>
        /// スクロールバーの位置
        /// </summary>
        private Vector2 _scrollPosition = Vector2.zero;

        /// <summary>
        /// GameObject配下の全てのマテリアル
        /// </summary>
        private List<MaterialData> allMaterials = null;

        /// <summary>
        /// Presetの設定をXMLから読み書きするオブジェクト
        /// </summary>
        private MToonSettings settings = null;

        /// <summary>
        /// LitColorの色指定
        /// </summary>
        private Color litColor = Color.white;

        /// <summary>
        /// ShadeColorの色指定
        /// </summary>
        private Color shadeColor = Color.white;

        /// <summary>
        /// EmissionColorの色指定
        /// </summary>
        private Color emissionColor = Color.white;

        /// <summary>
        /// プリセットのリスト
        /// </summary>
        private string[] presetList = {  "Save As.." };

        /// <summary>
        /// EmissionTextureのリスト
        /// </summary>
        private string[] emissionList = { "LitColorのTextureを適用する", "Textureを指定する" };

        /// <summary>
        /// プリセットの添字
        /// </summary>
        private int presetSelectedIndex = 0;

        /// <summary>
        ///  EmissionTextureの添字
        /// </summary>
        private int emissionSelectedIndex = 0;

        /// <summary>
        /// 新規プリセット名
        /// </summary>
        private string newPresetName = "";

        /// <summary>
        /// ウィンドウの表示
        /// </summary>
        [MenuItem("VRoidAvatarSetup/MToon Emission Setup", priority = 40)]
        static void ShowWindow()
        {
            var window = EditorWindow.GetWindow<MToonEmissionSetupUI>();
            window.titleContent = new GUIContent("MToonEmissionSetup");
        }

        /// <summary>
        /// レンダリングと GUI イベントのハンドリング
        /// </summary>
        void OnGUI()
        {

            EditorGUILayout.LabelField("Source", EditorStyles.boldLabel);
            
            EditorGUI.BeginChangeCheck();

            // ターゲット取得
            Target = (GameObject)EditorGUILayout.ObjectField("Target Avatar", Target, typeof(GameObject), true);

            if (Target != null && EditorGUI.EndChangeCheck())
            {
                // Target変更時

                // マテリアルの取得
                allMaterials = GetAllMaterials.GetAll(Target);
                for(int i = 0; i < IsSetProperty.Length; i++)
                {
                    IsSetProperty[i] = false;
                }

                //プリセットの取得
                settings = MToonSettings.load();
                updateList();
            }
            if (Target is null || allMaterials is null) return;

            EditorGUILayout.LabelField("EmissionPreset", EditorStyles.boldLabel);

            using (new GUILayout.HorizontalScope())
            {
                //プリセットの選択
                presetSelectedIndex = EditorGUILayout.Popup("Presets", presetSelectedIndex, presetList);
                if(presetSelectedIndex == presetList.Length - 1)
                {
                    
                    newPresetName = EditorGUILayout.TextField(newPresetName);
                    if (GUILayout.Button("Save As"))
                    {
                        // プリセットを名前を付けて保存
                        MToonSettings.Preset preset = new MToonSettings.Preset();
                        preset.name = newPresetName;
                        preset.litColor = litColor;
                        preset.shadeColor = shadeColor;
                        preset.emissionColor = emissionColor;
                        preset.emissionTextureType = emissionSelectedIndex;
                        preset.sourceTexturePath = AssetDatabase.GetAssetPath(SourceTexture);
                        settings.saveAs(preset);
                        updateList();
                        newPresetName = "";
                    }
                }
                else
                {

                    if (GUILayout.Button("Load"))
                    {
                        // プリセットの読み込み
                        litColor = settings.presets[presetSelectedIndex].litColor;
                        shadeColor = settings.presets[presetSelectedIndex].shadeColor;
                        emissionColor = settings.presets[presetSelectedIndex].emissionColor;
                        emissionSelectedIndex = settings.presets[presetSelectedIndex].emissionTextureType;
                        SourceTexture = AssetDatabase.LoadAssetAtPath<Texture>(settings.presets[presetSelectedIndex].sourceTexturePath);
                        updateList();
                    }
                    if (GUILayout.Button("Save"))
                    {
                        // プリセットを上書き保存
                        settings.presets[presetSelectedIndex].litColor = litColor;
                        settings.presets[presetSelectedIndex].shadeColor = shadeColor;
                        settings.presets[presetSelectedIndex].emissionColor = emissionColor;
                        settings.presets[presetSelectedIndex].emissionTextureType = emissionSelectedIndex;
                        settings.presets[presetSelectedIndex].sourceTexturePath = AssetDatabase.GetAssetPath(SourceTexture);
                        settings.update();
                        updateList();
                    }
                    if (GUILayout.Button("Delete"))
                    {
                        if (presetSelectedIndex > 1)
                        {
                            // プリセットを削除
                            settings.presets.RemoveAt(presetSelectedIndex);
                            settings.update();
                            updateList();
                        }
                    }
                }
            }


            EditorGUILayout.LabelField("EmissionSetting", EditorStyles.boldLabel);

            // LitColorを設定
            litColor = EditorGUILayout.ColorField("Lit Color, Alpha", litColor);
            // ShadeColorを設定
            shadeColor = EditorGUILayout.ColorField("Shade Color", shadeColor);
            // EmissionColorを設定
            emissionColor = EditorGUILayout.ColorField("Emission", emissionColor);


            // EmissionTextureListの選択
            emissionSelectedIndex = EditorGUILayout.Popup("EmissionTexture", emissionSelectedIndex, emissionList);

            if(emissionSelectedIndex == 1)
            {
                // EmissionTextureの選択
                SourceTexture = (Texture)EditorGUILayout.ObjectField("Source Texture", SourceTexture, typeof(Texture), true);
            }

            {
                EditorGUILayout.LabelField("Materials", EditorStyles.boldLabel);
                
                // マテリアル数の確認
                materialCount = allMaterials.Count;
                Array.Resize(ref IsSetProperty, materialCount);

                // マテリアル一覧
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
                    for (int i = 0; i < materialCount; i++)
                    {
                        string propertyName = allMaterials[i].material.name;
                        string labelText = propertyName;
                        GUIContent label = new GUIContent(labelText, propertyName);

                        // マテリアルを設定するか？
                        IsSetProperty[i] = EditorGUILayout.ToggleLeft(label, IsSetProperty[i]);
                    }
                    EditorGUILayout.EndScrollView();
                }
            }

            /*
              便利ボタン
            */
            using (new GUILayout.HorizontalScope())
            {
                // 一括設定
                if (GUILayout.Button("All")) setAllProperty(true);
                if (GUILayout.Button("Clear")) setAllProperty(false);

            }

            /*
              セットアップ開始
            */
            if (GUILayout.Button("Set"))
            {
                MaterialSetupUtil initMaterial = new MaterialSetupUtil(litColor,shadeColor, emissionColor);

                for(int i = 0; i < allMaterials.Count;  i++)
                {
                    if (IsSetProperty[i])
                    {
                        if(emissionSelectedIndex == 1)
                        {
                            // Emissionのテクスチャに指定したテクスチャを反映
                            initMaterial.SetUp(allMaterials[i], SourceTexture);
                        }
                        else
                        {
                            // EmissionのテクスチャにLitColorのテクスチャを反映（デフォルト動作） 
                            initMaterial.SetUp(allMaterials[i], null);
                        }
                    }
                }
            }
            /*
              リセット
            */
            if (GUILayout.Button("Reset"))
            {
                MaterialSetupUtil initMaterial = new MaterialSetupUtil(litColor, shadeColor, emissionColor);

                for (int i = 0; i < allMaterials.Count; i++)
                {
                    if (IsSetProperty[i])
                    {
                        initMaterial.Reset(allMaterials[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 一括設定関数
        /// </summary>
        /// <param name="isCopyProperty">プロパティをコピーするか？</param>
        private void setAllProperty(bool isCopyProperty)
        {
            for (int i = 0; i < materialCount; i++)
            {
                IsSetProperty[i] = isCopyProperty;
            }
        }
        /// <summary>
        /// presetの配列を更新する
        /// </summary>
        private void updateList()
        {
            presetList = new string[settings.presets.Count + 1];
            for (int i = 0; i < settings.presets.Count; i++)
            {
                presetList[i] = (i+1).ToString() + " : " + settings.presets[i].name;
            }
            presetList[settings.presets.Count] = "Save As..";
        }
    }
}